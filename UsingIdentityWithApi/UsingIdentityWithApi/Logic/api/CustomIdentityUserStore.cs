using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace UsingIdentityWithApi.Logic.api
{
    //public class CustomIdentityUserStore :
    //    UserStoreBase<ApiUser, string, IdentityUserClaim<string>, IdentityUserLogin<string>, IdentityUserToken<string>>
    //{
    //    public CustomIdentityUserStore(IdentityErrorDescriber describer) : base(describer)
    //    {
    //    }

    //    public override IQueryable<ApiUser> Users => throw new NotImplementedException();

    //    public override Task AddClaimsAsync(ApiUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task AddLoginAsync(ApiUser user, UserLoginInfo login, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task<IdentityResult> CreateAsync(ApiUser user, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task<IdentityResult> DeleteAsync(ApiUser user, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task<ApiUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task<ApiUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task<ApiUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task<IList<Claim>> GetClaimsAsync(ApiUser user, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task<IList<UserLoginInfo>> GetLoginsAsync(ApiUser user, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task<IList<ApiUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task RemoveClaimsAsync(ApiUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task RemoveLoginAsync(ApiUser user, string loginProvider, string providerKey, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task ReplaceClaimAsync(ApiUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override Task<IdentityResult> UpdateAsync(ApiUser user, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override Task AddUserTokenAsync(IdentityUserToken<string> token)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override Task<IdentityUserToken<string>> FindTokenAsync(ApiUser user, string loginProvider, string name, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override Task<ApiUser> FindUserAsync(string userId, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override Task<IdentityUserLogin<string>> FindUserLoginAsync(string userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override Task<IdentityUserLogin<string>> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override Task RemoveUserTokenAsync(IdentityUserToken<string> token)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
