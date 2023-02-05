using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace UsingIdentityWithApi.Logic.api
{
    public class CustomApiPasswordResetTokenProvider<TUser> : CustomDataProtectionTokenProvider<TUser> where TUser : class
    {
        public CustomApiPasswordResetTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<CustomApiPasswordResetTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {

        }
    }

    public class CustomApiPasswordResetTokenProviderOptions : CustomDataProtectionTokenProviderOptions
    {
        public CustomApiPasswordResetTokenProviderOptions()
        {
            Name = "CustomPasswordResetTokenProviderOptions";
            TokenLifespan = TimeSpan.FromHours(3);
        }
    }
}
