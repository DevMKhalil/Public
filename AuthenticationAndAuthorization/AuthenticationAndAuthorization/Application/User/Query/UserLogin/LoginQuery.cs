using AuthenticationAndAuthorization.Logic;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAndAuthorization.Application.User.Query.UserLogin
{
    public class LoginQuery : IRequest<Result<string>>
    {
        public Logic.UserLogin UserLogin { get; set; }
    }

    public class LoginQueryHandeler : IRequestHandler<LoginQuery, Result<string>>
    {
        private readonly IAuthenticationAndAuthorizationContext _context;
        private readonly IConfiguration _configuration;
        public LoginQueryHandeler(IAuthenticationAndAuthorizationContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Result<string>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await Authenticate(request.UserLogin);

            if (user is not null)
            {
                var token = Generate(user);
                return Result.Success(token);
            }

            return Result.Failure<string>("User Not Found");
        }

        private string Generate(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.GivenName,user.GivenName),
                new Claim(ClaimTypes.Surname,user.Surname),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserModel?> Authenticate(Logic.UserLogin userLogin)
        {
            var currentUser = await _context.UserModels.FirstOrDefaultAsync(o => o.UserName.ToLower() == userLogin.UserName.ToLower() &&
            userLogin.Password == o.Password);

            if (currentUser is not null)
                return currentUser;
            else
                return null;
        }
    }
}
