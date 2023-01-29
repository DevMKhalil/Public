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
