using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsingIdentityWithApi.Application.Users.Query.Login;
using UsingIdentityWithApi.Application.Users.Commands.Register;
using UsingIdentityWithApi.Logic.api;
using UsingIdentityWithApi.Application.Users.Query.ForgetPassword;
using UsingIdentityWithApi.Application.Users.Commands.ResetPassword;

namespace UsingIdentityWithApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUsersController : ControllerBase
    {
        private readonly ApiUserManager _userManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserClaimsPrincipalFactory<ApiUser> _claimsPrincipalFactory;
        private readonly IConfiguration _configuration;

        public ApiUsersController(
            ApiUserManager userManager,
            IHttpContextAccessor httpContextAccessor,
            IUserClaimsPrincipalFactory<ApiUser> claimsPrincipalFactory,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _claimsPrincipalFactory = claimsPrincipalFactory;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }


        [HttpGet("GenerateEmailConfirmationToken")]
        public async Task<IActionResult> GenerateEmailConfirmationToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is not null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var resetURL = Url.Action("ConfirmEmailAddress", "ApiUsers",
                    new { token = token, email = user.Email }, Request.Scheme);

                System.IO.File.WriteAllText("D:\\EmailConfirmationLink.txt", resetURL);

                return Ok(token);
            }
            return BadRequest("User Not Found");
        }


        [HttpPost("Register")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userDto.UserName);

                if (user is null)
                {
                    user = new ApiUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = userDto.UserName,
                        Email = userDto.Email,
                        PhoneNumber = userDto.PhoneNumber
                    };

                    var res = await _userManager.CreateAsync(user, userDto.Password);

                    if (res.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var resetURL = Url.Action("ConfirmEmailAddress", "ApiUsers",
                            new { token = token, email = user.Email }, Request.Scheme);

                        System.IO.File.WriteAllText("D:\\EmailConfirmationLink.txt", resetURL);

                        return Ok();
                    }
                    else
                        return BadRequest(res.Errors);
                }

                return Ok();
            }

            return BadRequest();
        }


        [HttpPost("ConfirmEmailAddress")]
        public async Task<IActionResult> ConfirmEmailAddress(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                    return Ok();
                else
                    return BadRequest(result.Errors);
            }
            return BadRequest("User Not Found");
        }


        [HttpGet("Login")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<string>> Login([FromQuery] LoginUserDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginDto.UserName);

                if (user is not null && !await _userManager.IsLockedOutAsync(user))
                {
                    if (await _userManager.CheckPasswordAsync(user, loginDto.Password))
                    {
                        if (!await _userManager.IsEmailConfirmedAsync(user))
                            return BadRequest("Email is not confirmed");

                        await _userManager.ResetAccessFailedCountAsync(user);

                        if (await _userManager.GetTwoFactorEnabledAsync(user))
                        {
                            var validateProviders = await _userManager.GetValidTwoFactorProvidersAsync(user);

                            //if (validateProviders.Contains(""))
                        }

                        var token = await GenerateJwtToken(user);

                        return Ok(token);
                    }

                    await _userManager.AccessFailedAsync(user);

                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        //send email to the user,notify him of lockout
                    }
                }

                return BadRequest("Invalid UserName Or Password");
            }

            return BadRequest();
        }


        private async Task<string> GenerateJwtToken(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var principal = await _claimsPrincipalFactory.CreateAsync(user);

            var claims = principal.Claims;

            var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpGet("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromQuery] ForgetPasswordDto forgetPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPassword.Email);

                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var resetURL = Url.Action("ResetPassword", "AspUsers", new { token = token, email = user.Email }, Request.Scheme);

                    System.IO.File.WriteAllText("D:\\resetLink.txt", resetURL);

                    return Ok(token);
                }
                else
                {
                    // email User and inform them that they do not have an account
                }
            }

            return Ok();
        }


        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto forgetPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPassword.Email);

                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, forgetPassword.Token, forgetPassword.Password);

                    if (!result.Succeeded)
                    {
                        return BadRequest(result.Errors);
                    }

                    if (await _userManager.IsLockedOutAsync(user))
                        await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);

                    return Ok();
                }
                return BadRequest("User Not Found");
            }
            return BadRequest();
        }


        [HttpGet("GeneratePhoneNumberConfirmationToken")]
        public async Task<IActionResult> GeneratePhoneNumberConfirmationToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is not null)
            {
                var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

                var resetURL = Url.Action("ConfirmPhoneNumber", "ApiUsers",
                    new { token = token, email = user.Email }, Request.Scheme);

                System.IO.File.WriteAllText("D:\\PhoneNumberConfirmationLink.txt", resetURL);

                return Ok(token);
            }
            return BadRequest("User Not Found");
        }


        [HttpPost("ConfirmPhoneNumber")]
        public async Task<IActionResult> ConfirmPhoneNumber(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                var result = await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, token);

                if (result.Succeeded)
                    return Ok();
                else
                    return BadRequest(result.Errors);
            }
            return BadRequest("User Not Found");
        }


        [HttpPost("EnableTwoFactor")]
        public async Task<IActionResult> EnableTwoFactor(string email,bool enabled)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                var result = await _userManager.SetTwoFactorEnabledAsync(user, enabled);

                if (result.Succeeded)
                    return Ok();
                else
                    return BadRequest(result.Errors);
            }
            return BadRequest("User Not Found");
        }
    }
}
