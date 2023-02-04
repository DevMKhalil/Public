using Microsoft.AspNetCore.Identity;

namespace UsingIdentityWithApi.Logic.Shared
{
    public class CustomePasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var userName = await manager.GetUserNameAsync(user);

            if (userName.ToLower() == password.ToLower())
                return IdentityResult.Failed(new IdentityError { Description = "Password cannot contain username" });

            if (password.Contains("password"))
                return IdentityResult.Failed(new IdentityError { Description = "Password cannot contain password" });

            return IdentityResult.Success;
        }
    }
}
