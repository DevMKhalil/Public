using AuthenticationAndAuthorization.Logic;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAndAuthorization.Application
{
    public interface IAuthenticationAndAuthorizationContext
    {
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<Movie> Movies { get; set; }
        Task<Result> SaveChangesWithValidation(CancellationToken cancellationToken = default(CancellationToken));
    }
}
