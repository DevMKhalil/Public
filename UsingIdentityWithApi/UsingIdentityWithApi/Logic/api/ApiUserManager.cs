using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Resources;

namespace UsingIdentityWithApi.Logic.api
{
    public class ApiUserManager : UserManager<ApiUser>
    {
        private readonly IUserTwoFactorTokenProvider<ApiUser> _userTwoFactorTokenProvider;

        public ApiUserManager(
            IUserStore<ApiUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApiUser> passwordHasher,
            IEnumerable<IUserValidator<ApiUser>> userValidators,
            IEnumerable<IPasswordValidator<ApiUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<ApiUser>> logger,
            IUserTwoFactorTokenProvider<ApiUser> userTwoFactorTokenProvider ) : base(
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger)
        {
            _userTwoFactorTokenProvider = userTwoFactorTokenProvider;
        }

        #region GenerateEmailConfirmationToken
        public override Task<string> GenerateEmailConfirmationTokenAsync(ApiUser user)
        {
            return GenerateUserTokenAsync(user, string.Empty, ConfirmEmailTokenPurpose);
        }

        public override Task<string> GenerateUserTokenAsync(ApiUser user, string tokenProvider, string purpose)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (_userTwoFactorTokenProvider is null)
            {
                throw new ArgumentNullException(nameof(tokenProvider));
            }

            return _userTwoFactorTokenProvider.GenerateAsync(purpose, this, user);
        }
        #endregion

        #region ConfirmEmail
        public async override Task<IdentityResult> ConfirmEmailAsync(ApiUser user, string token)
        {
            ThrowIfDisposed();
            var store = GetEmailStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (!await VerifyUserTokenAsync(user, Options.Tokens.EmailConfirmationTokenProvider, ConfirmEmailTokenPurpose, token))
            {
                return IdentityResult.Failed(ErrorDescriber.InvalidToken());
            }
            await store.SetEmailConfirmedAsync(user, true, CancellationToken);
            return await UpdateUserAsync(user);
        }

        private IUserEmailStore<ApiUser> GetEmailStore(bool throwOnFail = true)
        {
            var cast = Store as IUserEmailStore<ApiUser>;
            if (throwOnFail && cast == null)
            {
                throw new NotSupportedException("StoreNotIUserEmailStore");
            }
            return cast;
        }

        public override async Task<bool> VerifyUserTokenAsync(ApiUser user, string tokenProvider, string purpose, string token)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (tokenProvider == null)
            {
                throw new ArgumentNullException(nameof(tokenProvider));
            }

            if (_userTwoFactorTokenProvider is null)
            {
                throw new NotSupportedException($"No Supported Token Provider {tokenProvider} For User{nameof(ApiUser)} ");
            }
            // Make sure the token is valid
            var result = await _userTwoFactorTokenProvider.ValidateAsync(purpose, token, this, user);

            if (!result)
            {
                Logger.LogWarning("VerifyUserTokenFailed", "VerifyUserTokenAsync() failed with purpose: {purpose} for user.", purpose);
            }
            return result;
        } 
        #endregion
    }
}
