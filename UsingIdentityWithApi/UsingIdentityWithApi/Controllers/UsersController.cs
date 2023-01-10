
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using UsingIdentityWithApi.Application.Users.Query.Login;
using UsingIdentityWithApi.Application.Users.Query.Register;
using UsingIdentityWithApi.Logic;

namespace UsingIdentityWithApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiUserManager _userManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _aspUserManager;
        readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(ApiUserManager userManager, IHttpContextAccessor httpContextAccessor, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> aspUserManager)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _aspUserManager = aspUserManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
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
                    };

                    var res = await _userManager.CreateAsync(user, userDto.Password);

                    if (res.Succeeded)
                        return Ok();
                    else
                        return BadRequest(res.Errors);
                }
    
                return Ok();
            }
    
            return BadRequest();
        }



        [HttpPost("AspRegister")]
        public async Task<IActionResult> AspRegister([FromBody] RegisterUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _aspUserManager.FindByNameAsync(userDto.UserName);

                if (user is null)
                {
                    user = new IdentityUser
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


        [HttpGet("Login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromQuery]LoginUserDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginDto.UserName);

                if (user is not null && await _userManager.CheckPasswordAsync(user,loginDto.Password))
                {
                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    await _httpContextAccessor.HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));

                    return Ok();
                }

                return BadRequest("Invalid UserName Or Password");
            }

            return BadRequest();
        }
    }
}
