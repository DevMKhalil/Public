using AuthenticationAndAuthorization.Application;
using AuthenticationAndAuthorization.Logic;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuthenticationAndAuthorization.Data
{
    public class AuthenticationAndAuthorizationContext : DbContext, IAuthenticationAndAuthorizationContext
    {
        public AuthenticationAndAuthorizationContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<UserModel> UserModels { get; set; }

        public async Task<Result> SaveChangesWithValidation(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
            catch (DbUpdateException dbExce)
            {
                return Result.Failure(dbExce.Message);
            }
            catch (Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
