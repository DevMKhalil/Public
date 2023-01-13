using Microsoft.AspNetCore.Identity;

namespace UsingIdentityWithApi.Logic
{
    public class AspUser : IdentityUser<string>
    {
        public string Locale { get; set; } = "ar-EG";
    }
}
