using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsingIdentityWithApi.Application.Users.Commands.Register;
using UsingIdentityWithApi.Application.Users.Commands.ResetPassword;
using UsingIdentityWithApi.Application.Users.Query.ForgetPassword;
using UsingIdentityWithApi.Application.Users.Query.Login;
using UsingIdentityWithApi.Logic.api;
using UsingIdentityWithApi.Logic.asp;

namespace UsingIdentityWithApi.Controllers
{
    [Route("Asp/[controller]")]
    [ApiController]
    public class AspUsersController : ControllerBase
    {
        private readonly UserManager<AspUser> _aspUserManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserClaimsPrincipalFactory<AspUser> _claimsPrincipalFactory;
        private readonly IConfiguration _configuration;

        public AspUsersController(
            IHttpContextAccessor httpContextAccessor,
            UserManager<AspUser> aspUserManager,
            IUserClaimsPrincipalFactory<AspUser> claimsPrincipalFactory,
            IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _aspUserManager = aspUserManager;
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
            var user = await _aspUserManager.FindByIdAsync(userId);

            if (user is not null)
            {
                var token = await _aspUserManager.GenerateEmailConfirmationTokenAsync(user);

                var resetURL = Url.Action("ConfirmEmailAddress", "ApiUsers",
                    new { token = token, email = user.Email }, Request.Scheme);

                System.IO.File.WriteAllText("D:\\EmailConfirmationLink.txt", resetURL);

                return Ok(token);
            }
            return BadRequest("User Not Found");
        }


        [HttpPost("AspRegister")]
        public async Task<IActionResult> AspRegister([FromBody] RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _aspUserManager.FindByNameAsync(userDto.UserName);

                if (user is null)
                {
                    user = new AspUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = userDto.UserName,
                        Email = userDto.Email,
                        PhoneNumber = userDto.PhoneNumber
                    };

                    var res = await _aspUserManager.CreateAsync(user, userDto.Password);

                    if (res.Succeeded)
                    {
                        var token = await _aspUserManager.GenerateEmailConfirmationTokenAsync(user);

                        var resetURL = Url.Action("ConfirmEmailAddress", "AspUsers",
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
            var user = await _aspUserManager.FindByEmailAsync(email);

            if (user is not null)
            {
                var result = await _aspUserManager.ConfirmEmailAsync(user, token);

                if (result.Succeeded)
                    return Ok();
                else
                    return BadRequest(result.Errors);
            }
            return BadRequest("User Not Found");
        }


        [HttpGet("AspLogin")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromQuery] LoginUserDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _aspUserManager.FindByNameAsync(loginDto.UserName);

                if (user is not null && !await _aspUserManager.IsLockedOutAsync(user))
                {
                    if (await _aspUserManager.CheckPasswordAsync(user, loginDto.Password))
                    {
                        if (!await _aspUserManager.IsEmailConfirmedAsync(user))
                            return BadRequest("Email is not confirmed");

                        await _aspUserManager.ResetAccessFailedCountAsync(user);

                        if (await _aspUserManager.GetTwoFactorEnabledAsync(user))
                            return RedirectToAction("GetValidProviders", new { userId = user.Id });

                        var token = await GenerateJwtToken(user);

                        return Ok(token);
                    }

                    await _aspUserManager.AccessFailedAsync(user);

                    if (await _aspUserManager.IsLockedOutAsync(user))
                    {
                        //send email to the user,notify him of lockout
                    }
                }
                return BadRequest("Invalid UserName Or Password");
            }

            return BadRequest();
        }


        [HttpGet("GetValidProviders")]
        public async Task<IActionResult> GetValidProviders(string userId)
        {
            var user = await _aspUserManager.FindByIdAsync(userId);

            if (user is not null)
            {
                var result = await _aspUserManager.GetValidTwoFactorProvidersAsync(user);

                return Ok(result);
            }
            return BadRequest("User Not Found");
        }


        [HttpGet("GenerateTwoStepToken")]
        public async Task<IActionResult> GenerateTwoFactorToken(string userId, string provider)
        {
            var user = await _aspUserManager.FindByIdAsync(userId);

            if (user is not null)
            {
                var validProviders = await _aspUserManager.GetValidTwoFactorProvidersAsync(user);

                if (validProviders.Contains(provider))
                {
                    var token = await _aspUserManager.GenerateTwoFactorTokenAsync(user, provider);

                    System.IO.File.WriteAllText("D:\\TwoFactorToken.txt", token);

                    return Ok(token);
                }
            }
            return BadRequest("User Not Found");
        }


        [HttpPost("ValidateTwoStepToken")]
        public async Task<IActionResult> ValidateTwoFactorToken(string userId, string provider, string token)
        {
            var user = await _aspUserManager.FindByIdAsync(userId);

            if (user is not null)
            {
                var isValid = await _aspUserManager.VerifyTwoFactorTokenAsync(user, provider, token);

                if (isValid)
                {
                    var jwtToken = await GenerateJwtToken(user);

                    return Ok(jwtToken);
                }
                return BadRequest("Invalid Token");
            }
            return BadRequest("User Not Found");
        }


        private async Task<string> GenerateJwtToken(AspUser user)
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
                var user = await _aspUserManager.FindByEmailAsync(forgetPassword.Email);

                if (user is not null)
                {
                    var token = await _aspUserManager.GeneratePasswordResetTokenAsync(user);

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
                var user = await _aspUserManager.FindByEmailAsync(forgetPassword.Email);

                if (user is not null)
                {
                    var result = await _aspUserManager.ResetPasswordAsync(user, forgetPassword.Token, forgetPassword.Password);

                    if (!result.Succeeded)
                    {
                        return BadRequest(result.Errors);
                    }

                    if (await _aspUserManager.IsLockedOutAsync(user))
                        await _aspUserManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);

                    return Ok();
                }
                return BadRequest("User Not Found");
            }
            return BadRequest();
        }


        [HttpGet("GeneratePhoneNumberConfirmationToken")]
        public async Task<IActionResult> GeneratePhoneNumberConfirmationToken(string userId)
        {
            var user = await _aspUserManager.FindByIdAsync(userId);

            if (user is not null)
            {
                var token = await _aspUserManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

                var resetURL = Url.Action("ConfirmPhoneNumber", "AspUsers",
                    new { token = token, email = user.Email }, Request.Scheme);

                System.IO.File.WriteAllText("D:\\PhoneNumberConfirmationLink.txt", resetURL);

                return Ok(token);
            }
            return BadRequest("User Not Found");
        }


        [HttpPost("ConfirmPhoneNumber")]
        public async Task<IActionResult> ConfirmPhoneNumber(string token, string email)
        {
            var user = await _aspUserManager.FindByEmailAsync(email);

            if (user is not null)
            {
                var result = await _aspUserManager.ChangePhoneNumberAsync(user, user.PhoneNumber, token);

                if (result.Succeeded)
                    return Ok();
                else
                    return BadRequest(result.Errors);
            }
            return BadRequest("User Not Found");
        }


        [HttpPost("EnableTwoFactor")]
        public async Task<IActionResult> EnableTwoFactor(string email, bool enabled)
        {
            var user = await _aspUserManager.FindByEmailAsync(email);

            if (user is not null)
            {
                var result = await _aspUserManager.SetTwoFactorEnabledAsync(user, enabled);

                if (result.Succeeded)
                    return Ok();
                else
                    return BadRequest(result.Errors);
            }
            return BadRequest("User Not Found");
        }
    }
}
