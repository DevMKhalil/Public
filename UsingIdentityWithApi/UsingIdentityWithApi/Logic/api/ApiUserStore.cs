using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsingIdentityWithApi.Application;

namespace UsingIdentityWithApi.Logic.api
{
    public class ApiUserStore : IUserStore<ApiUser>, IUserPasswordStore<ApiUser>, IUserEmailStore<ApiUser>, IUserSecurityStampStore<ApiUser>
    {
        private readonly IUsingIdentityWithApiContext _context;

        public ApiUserStore(IUsingIdentityWithApiContext context)
        {
            _context = context;
        }
        public async Task<IdentityResult> CreateAsync(ApiUser user, CancellationToken cancellationToken)
        {
            _context.XUsers.Add(user);

            var res = await _context.SaveChangesWithValidation(cancellationToken);

            if (res.IsSuccess) { return IdentityResult.Success; }
            else { return IdentityResult.Failed(new IdentityError[] { new IdentityError { Description = res.Error } }); }
        }

        public async Task<IdentityResult> DeleteAsync(ApiUser user, CancellationToken cancellationToken)
        {
            _context.XUsers.Remove(user);

            var res = await _context.SaveChangesWithValidation(cancellationToken);

            if (res.IsSuccess) { return IdentityResult.Success; }
            else { return IdentityResult.Failed(new IdentityError[] { new IdentityError { Description = res.Error } }); }
        }

        public void Dispose()
        {
        }

        public async Task<ApiUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await _context.XUsers.FirstOrDefaultAsync(x => x.NormalizedEmail == normalizedEmail);
        }

        public async Task<ApiUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.XUsers.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<ApiUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _context.XUsers.FirstOrDefaultAsync(x => x.NormalizedUserName == normalizedUserName);
        }

        public Task<string> GetEmailAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<string> GetNormalizedEmailAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetSecurityStampAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task<string> GetUserIdAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetEmailAsync(ApiUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task SetEmailConfirmedAsync(ApiUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetNormalizedEmailAsync(ApiUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(ApiUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(ApiUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetSecurityStampAsync(ApiUser user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(ApiUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(ApiUser user, CancellationToken cancellationToken)
        {
            var res = await _context.SaveChangesWithValidation(cancellationToken);

            if (res.IsSuccess) { return IdentityResult.Success; }
            else { return IdentityResult.Failed(new IdentityError[] { new IdentityError { Description = res.Error } }); }
        }
    }
}
