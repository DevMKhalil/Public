using Microsoft.AspNetCore.Identity;

namespace UsingIdentityWithApi.Logic.asp
{
    public class AspUser : IdentityUser<string>
    {
        public string Locale { get; set; } = "ar-SA";
    }
}
