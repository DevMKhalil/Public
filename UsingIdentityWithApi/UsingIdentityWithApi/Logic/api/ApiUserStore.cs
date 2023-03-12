using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsingIdentityWithApi.Application;

namespace UsingIdentityWithApi.Logic.api
{
    public class ApiUserStore<TApiUserTokens> : 
        IUserStore<ApiUser>, 
        IUserPasswordStore<ApiUser>, 
        IUserSecurityStampStore<ApiUser>, 
        IUserEmailStore<ApiUser>, 
        IUserLockoutStore<ApiUser>,
        IUserPhoneNumberStore<ApiUser>,
        IUserTwoFactorStore<ApiUser>,
        IUserAuthenticationTokenStore<ApiUser>,
        IUserAuthenticatorKeyStore<ApiUser>
        where TApiUserTokens : IdentityUserToken<TKey>, new()
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

        public Task<int> GetAccessFailedCountAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<string> GetAuthenticatorKeyAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return GetTokenAsync(user, InternalLoginProvider, AuthenticatorKeyTokenName, cancellationToken);
        }

        public Task<string> GetEmailAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<bool> GetLockoutEnabledAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.LockoutEnd);
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

        public Task<string> GetPhoneNumberAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task<string> GetSecurityStampAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public Task<string> GetTokenAsync(ApiUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApiUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
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

        public Task<int> IncrementAccessFailedCountAsync(ApiUser user, CancellationToken cancellationToken)
        {
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task RemoveTokenAsync(ApiUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(ApiUser user, CancellationToken cancellationToken)
        {
            user.AccessFailedCount = default(int);
            return Task.CompletedTask;
        }

        public Task SetAuthenticatorKeyAsync(ApiUser user, string key, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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

        public Task SetLockoutEnabledAsync(ApiUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.LockoutEnabled = enabled;
            return Task.CompletedTask;
        }

        public Task SetLockoutEndDateAsync(ApiUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            user.LockoutEnd = lockoutEnd;
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

        public Task SetPhoneNumberAsync(ApiUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.CompletedTask;
        }

        public Task SetPhoneNumberConfirmedAsync(ApiUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetSecurityStampAsync(ApiUser user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;
            return Task.CompletedTask;
        }

        public Task SetTokenAsync(ApiUser user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(ApiUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
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

        #region StoreFunctions
        protected Task<ApiUserTokens> FindTokenAsync(ApiUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {

        };
        #endregion
    }
}
