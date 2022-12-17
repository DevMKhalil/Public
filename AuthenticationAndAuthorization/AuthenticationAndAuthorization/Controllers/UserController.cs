using AuthenticationAndAuthorization.Application.User.Query.GetCurrentUser;
using AuthenticationAndAuthorization.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Admins")]
        [Authorize(Roles = SharedConstants.admin)]
        public async Task<ActionResult<string>> AdminsEndPoint()
        {
            var result = await _mediator.Send(new GetCurrentUserQuery());

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Error);
        }

        [HttpGet("Sellers")]
        [Authorize(Roles = SharedConstants.seller)]
        public async Task<ActionResult<string>> SellerEndPoint()
        {
            var result = await _mediator.Send(new GetCurrentUserQuery());

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Error);
        }

        [HttpGet("AdminsAndSellers")]
        [Authorize(Roles = $"{SharedConstants.seller},{SharedConstants.admin}")]
        public async Task<ActionResult<string>> AdminsSellerEndPoint()
        {
            var result = await _mediator.Send(new GetCurrentUserQuery());

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Error);
        }
    }
}
