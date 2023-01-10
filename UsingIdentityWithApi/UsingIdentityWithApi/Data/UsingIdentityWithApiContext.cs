using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UsingIdentityWithApi.Application;
using UsingIdentityWithApi.Logic;

namespace UsingIdentityWithApi.Data
{
    public class UsingIdentityWithApiContext : DbContext, IUsingIdentityWithApiContext
    {
        public UsingIdentityWithApiContext(DbContextOptions<UsingIdentityWithApiContext> options) : base(options)
        {

        }
        public DbSet<ApiUser> XUsers { get; set; }

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
