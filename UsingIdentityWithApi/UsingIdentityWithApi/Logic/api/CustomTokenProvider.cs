using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace UsingIdentityWithApi.Logic.api
{
    public class CustomTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public CustomTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<PasswordResetTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {

        }
    }

    public class PasswordResetTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public PasswordResetTokenProviderOptions()
        {
            Name = "PasswordResetTokenProvider";
            TokenLifespan = TimeSpan.FromHours(3);
        }
    }
}
