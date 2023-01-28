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
                        Email = userDto.Email
                    };

                    var res = await _aspUserManager.CreateAsync(user, userDto.Password);

                    if (res.Succeeded)
                        return Ok();
                    else
                        return BadRequest(res.Errors);
                }

                return Ok();
            }

            return BadRequest();
        }


        [HttpGet("AspLogin")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromQuery]LoginUserDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _aspUserManager.FindByNameAsync(loginDto.UserName);

                if (user is not null && await _aspUserManager.CheckPasswordAsync(user,loginDto.Password))
                {
                    var token = await GenerateJwtToken(user);

                    return Ok(token);
                }

                return BadRequest("Invalid UserName Or Password");
            }

            return BadRequest();
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


        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromQuery] ForgetPasswordDto forgetPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _aspUserManager.FindByEmailAsync(forgetPassword.Email);

                if (user is not null)
                {
                    var token = await _aspUserManager.GeneratePasswordResetTokenAsync(user);

                    var resetURL = Url.Action("ResetPassword", "AspUsers", new { token = token, email = user.Email}, Request.Scheme);

                    System.IO.File.WriteAllText("D:\\resetLink.txt", resetURL);
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
                    var result = await _aspUserManager.ResetPasswordAsync(user,forgetPassword.Token,forgetPassword.Password);

                    if (!result.Succeeded)
                    {
                        return BadRequest(result.Errors);
                    }
                    return Ok();
                }
                return BadRequest("User Not Found");
            }
            return BadRequest();
        }
    }
}
