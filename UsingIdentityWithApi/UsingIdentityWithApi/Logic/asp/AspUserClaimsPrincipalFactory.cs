using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace UsingIdentityWithApi.Logic.asp
{
    public class AspUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AspUser>
    {
        public AspUserClaimsPrincipalFactory(UserManager<AspUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AspUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Locale",user.Locale));
            return identity;
        }
    }
}
