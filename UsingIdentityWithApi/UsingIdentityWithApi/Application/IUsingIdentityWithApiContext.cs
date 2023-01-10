using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using UsingIdentityWithApi.Logic;

namespace UsingIdentityWithApi.Application
{
    public interface IUsingIdentityWithApiContext
    {
        public DbSet<ApiUser> XUsers { get; set; }

        Task<Result> SaveChangesWithValidation(CancellationToken cancellationToken = default(CancellationToken));
    }
}
