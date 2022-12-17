using AuthenticationAndAuthorization.Application.User.Query.UserLogin;
using AuthenticationAndAuthorization.Logic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<string>> Login([FromBody] UserLogin userLogin)
        {
            var result = await _mediator.Send(new LoginQuery { UserLogin = userLogin});

            if (result.IsSuccess)
                return Ok(result.Value);
            
            return NotFound(result.Error);
        }
    }
}
