using BuberDinner.Application.services.Authentication;
using BuberDinner.contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuberDinner.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationController(IAuthenticationService _authenticationService)
        {
            authenticationService = _authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            var response = new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.EMail,
                authResult.Token);
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = authenticationService.Login(
                            request.Email,
                            request.Password);

            var response = new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.EMail,
                authResult.Token);
            return Ok(response);
        }
    }
}
