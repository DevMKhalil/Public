using AuthenticationAndAuthorization.Logic;
using CSharpFunctionalExtensions;
using MediatR;
using System.Security.Claims;

namespace AuthenticationAndAuthorization.Application.User.Query.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<Result<string>>
    {
    }

    public class GetCurrentUserQueryHandeler : IRequestHandler<GetCurrentUserQuery, Result<string>>
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        public GetCurrentUserQueryHandeler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Result<string>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor.HttpContext.User.Identity is not ClaimsIdentity identity)
                return Result.Failure<string>("User Not Found");

            var claims = identity.Claims;

            var user = new UserModel
            {
                UserName = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                EmailAddress = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value,
                GivenName = claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                Surname = claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value,
                Role = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
            };

            return Result.Success($"Hi {user.GivenName}, you are an {user.Role}");
        }
    }
}
