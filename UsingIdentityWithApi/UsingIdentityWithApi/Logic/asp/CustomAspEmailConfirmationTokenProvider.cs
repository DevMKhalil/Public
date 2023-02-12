using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace UsingIdentityWithApi.Logic.asp
{
    public class CustomAspEmailConfirmationTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public CustomAspEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<CustomAspEmailConfirmationTokenProviderOptions> options,
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

    public class CustomAspEmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public CustomAspEmailConfirmationTokenProviderOptions()
        {
            Name = "CustomAspEmailConfirmationTokenProvider";
            TokenLifespan = TimeSpan.FromDays(3);
        }
    }
}
