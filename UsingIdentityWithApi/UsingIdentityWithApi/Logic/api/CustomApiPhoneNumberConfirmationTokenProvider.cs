using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace UsingIdentityWithApi.Logic.api
{
    public class CustomApiPhoneNumberConfirmationTokenProvider<TUser> : PhoneNumberTokenProvider<TUser> where TUser : class
    {
        public CustomApiPhoneNumberConfirmationTokenProvider()
            : base()
        {

        }
    }
}
