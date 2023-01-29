using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace UsingIdentityWithApi.Logic.api
{
    public class ApiUserManager : UserManager<ApiUser>
    {
        /// <summary>
        /// Constructs a new instance of <see cref="ApiUserManager`1"/>.
        /// </summary>
        /// <param name="store">The persistence store the manager will operate over.</param>
        /// <param name="optionsAccessor">The accessor used to access the <see cref="IdentityOptions"/>.</param>
        /// <param name="passwordHasher">The password hashing implementation to use when saving passwords.</param>
        /// <param name="userValidators">A collection of <see cref="Microsoft.AspNetCore.Identity.IUserValidator`1"/> to validate users against.</param>
        /// <param name="passwordValidators">A collection of <see cref="Microsoft.AspNetCore.Identity.IPasswordValidator`1"/> to validate passwords against.</param>
        /// <param name="keyNormalizer">The <see cref="ILookupNormalizer"/> to use when generating index keys for users.</param>
        /// <param name="errors">The <see cref="IdentityErrorDescriber"/> used to provider error messages.</param>
        /// <param name="services">The <see cref="IServiceProvider"/> used to resolve services.</param>
        /// <param name="logger">The logger used to log messages, warnings and errors.</param>
        public ApiUserManager(IUserStore<ApiUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApiUser> passwordHasher, IEnumerable<IUserValidator<ApiUser>> userValidators, IEnumerable<IPasswordValidator<ApiUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApiUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            //RegisterTokenProvider(TokenOptions.DefaultProvider, new CustomDataProtectionTokenProvider<ApiUser>());
            //RegisterTokenProvider(TokenOptions.DefaultEmailProvider, new EmailTokenProvider<ApiUser>());
            //RegisterTokenProvider(TokenOptions.DefaultPhoneProvider, new PhoneNumberTokenProvider<ApiUser>());
            //RegisterTokenProvider(TokenOptions.DefaultAuthenticatorProvider, new AuthenticatorTokenProvider<ApiUser>());
        }
    }
}
