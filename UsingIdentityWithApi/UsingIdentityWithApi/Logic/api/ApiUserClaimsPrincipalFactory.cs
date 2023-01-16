using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace UsingIdentityWithApi.Logic.api
{
    public class ApiUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApiUser>
    {
        public ApiUserClaimsPrincipalFactory(UserManager<ApiUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApiUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Locale",user.Locale));
            return identity;
        }
    }
}
