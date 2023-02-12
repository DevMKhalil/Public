using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace UsingIdentityWithApi.Logic.api
{
    public class CustomApiEmailConfirmationTokenProvider<TUser> : CustomDataProtectionTokenProvider<TUser> where TUser : class
    {
        public CustomApiEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<CustomApiEmailConfirmationTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {

        }

        public async override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
        {
            var email = await manager.GetEmailAsync(user);

            return !string.IsNullOrWhiteSpace(email) && await manager.IsEmailConfirmedAsync(user);
        }
    }

    public class CustomApiEmailConfirmationTokenProviderOptions : CustomDataProtectionTokenProviderOptions
    {
        public CustomApiEmailConfirmationTokenProviderOptions()
        {
            Name = "ApiEmailConfirmationTokenProviderOptions";
            TokenLifespan = TimeSpan.FromDays(3);
        }
    }
}
