using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace UsingIdentityWithApi.Logic.asp
{
    public class CustomAspPasswordResetTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public CustomAspPasswordResetTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<CustomAspPasswordResetTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {

        }
    }

    public class CustomAspPasswordResetTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public CustomAspPasswordResetTokenProviderOptions()
        {
            //Name = "CustomPasswordResetTokenProviderOptions";
            //TokenLifespan = TimeSpan.FromHours(3);
        }
    }
}
