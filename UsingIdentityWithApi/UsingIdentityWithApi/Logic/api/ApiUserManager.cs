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
            //RegisterTokenProvider(TokenOptions.DefaultProvider, new CustomDataProtectionTokenProvider<ApiUser>());
            //RegisterTokenProvider(TokenOptions.DefaultEmailProvider, new EmailTokenProvider<ApiUser>());
            //RegisterTokenProvider(TokenOptions.DefaultPhoneProvider, new PhoneNumberTokenProvider<ApiUser>());
            //RegisterTokenProvider(TokenOptions.DefaultAuthenticatorProvider, new AuthenticatorTokenProvider<ApiUser>());
        }

        public override Task<string> GenerateEmailConfirmationTokenAsync(ApiUser user)
        {
            return GenerateUserTokenAsync(user,string.Empty, "EmailConfirmation");
        }

        public override Task<string> GenerateUserTokenAsync(ApiUser user, string tokenProvider, string purpose)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (_userTwoFactorTokenProvider == null)
            {
                throw new ArgumentNullException(nameof(tokenProvider));
            }

            return _userTwoFactorTokenProvider.GenerateAsync(purpose, this, user);
        }
    }
}
