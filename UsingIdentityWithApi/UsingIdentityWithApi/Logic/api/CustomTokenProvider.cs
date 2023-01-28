using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace UsingIdentityWithApi.Logic.api
{
    public class CustomTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public CustomTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<CustomTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {

        }
    }

    public class CustomTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public CustomTokenProviderOptions()
        {
            Name = "PasswordResetTokenProvider";
            TokenLifespan = TimeSpan.FromHours(3);
        }
    }
}
