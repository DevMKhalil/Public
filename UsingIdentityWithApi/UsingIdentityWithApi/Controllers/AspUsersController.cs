using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using UsingIdentityWithApi.Application.Users.Query.Login;
using UsingIdentityWithApi.Application.Users.Query.Register;
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

        public AspUsersController(
            IHttpContextAccessor httpContextAccessor,
            UserManager<AspUser> aspUserManager,
            IUserClaimsPrincipalFactory<AspUser> claimsPrincipalFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _aspUserManager = aspUserManager;
            _claimsPrincipalFactory = claimsPrincipalFactory;
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
                    var principal = await _claimsPrincipalFactory.CreateAsync(user);

                    await _httpContextAccessor.HttpContext.SignInAsync("Identity.Application", new ClaimsPrincipal(principal));

                    return Ok();
                }

                return BadRequest("Invalid UserName Or Password");
            }

            return BadRequest();
        }
    }
}
