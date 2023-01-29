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
