using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UsingIdentityWithApi.Application;
using UsingIdentityWithApi.Logic.api;
using UsingIdentityWithApi.Logic.asp;

namespace UsingIdentityWithApi.Data
{
    public class UsingIdentityWithApiContext : IdentityDbContext<AspUser, IdentityRole<string>, string>, IUsingIdentityWithApiContext
    {
        public UsingIdentityWithApiContext(DbContextOptions<UsingIdentityWithApiContext> options) : base(options)
        {

        }
        public DbSet<ApiUser> XUsers { get; set; }
        //public DbSet<IdentityUser> User { get => this.Users; }

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
