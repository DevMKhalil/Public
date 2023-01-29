using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace UsingIdentityWithApi.Logic.api
{
    public class CustomDataProtectionTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public CustomDataProtectionTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<CustomDataProtectionTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {

        }
    }

    public class CustomDataProtectionTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public CustomDataProtectionTokenProviderOptions()
        {
            Name = "ApiCustomeProviderOptions";
        }
    }
}
