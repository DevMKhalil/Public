using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsingIdentityWithApi.Application.Users.Query.Register;
using UsingIdentityWithApi.Logic;

namespace UsingIdentityWithApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;

        public UsersController(UserManager<ApiUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }


        [HttpPost("Register")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
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
    }
}
