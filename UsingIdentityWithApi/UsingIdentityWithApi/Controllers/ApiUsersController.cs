using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using UsingIdentityWithApi.Application.Users.Query.Login;
using UsingIdentityWithApi.Application.Users.Query.Register;
using UsingIdentityWithApi.Logic.api;

namespace UsingIdentityWithApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUsersController : ControllerBase
    {
        private readonly ApiUserManager _userManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserClaimsPrincipalFactory<ApiUser> _claimsPrincipalFactory;

        public ApiUsersController(
            ApiUserManager userManager,
            IHttpContextAccessor httpContextAccessor,
            IUserClaimsPrincipalFactory<ApiUser> claimsPrincipalFactory)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _claimsPrincipalFactory = claimsPrincipalFactory;
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


        [HttpGet("Login")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromQuery]LoginUserDto loginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginDto.UserName);

                if (user is not null && await _userManager.CheckPasswordAsync(user,loginDto.Password))
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
