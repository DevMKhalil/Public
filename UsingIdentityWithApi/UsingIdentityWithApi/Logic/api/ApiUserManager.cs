using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Resources;

namespace UsingIdentityWithApi.Logic.api
{
    public class ApiUserManager : UserManager<ApiUser>
    {
        public ApiUserManager(
            IUserStore<ApiUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApiUser> passwordHasher,
            IEnumerable<IUserValidator<ApiUser>> userValidators,
            IEnumerable<IPasswordValidator<ApiUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<ApiUser>> logger) : base(
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger)
        {

        }

        #region Generate Phone Number Confirmation Token
        public override Task<string> GenerateChangePhoneNumberTokenAsync(ApiUser user, string phoneNumber)
        {
            ThrowIfDisposed();
            return GenerateUserTokenAsync(user, CustomTokenOptions.ApiCustomPhoneNumberConfirmationTokenProvider, ChangePhoneNumberTokenPurpose + ":" + phoneNumber);
        }
        #endregion

        #region Confirm Phone Number
        public override Task<bool> VerifyChangePhoneNumberTokenAsync(ApiUser user, string token, string phoneNumber)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Make sure the token is valid and the stamp matches
            return VerifyUserTokenAsync(user, CustomTokenOptions.ApiCustomPhoneNumberConfirmationTokenProvider, ChangePhoneNumberTokenPurpose + ":" + phoneNumber, token);
        } 
        #endregion

        #region Generate Email Confirmation Token
        public override Task<string> GenerateEmailConfirmationTokenAsync(ApiUser user)
        {
            ThrowIfDisposed();
            return GenerateUserTokenAsync(user, CustomTokenOptions.ApiCustomEmailConfirmationTokenProvider, ConfirmEmailTokenPurpose);
        }
        #endregion

        #region Confirm Email
        public async override Task<IdentityResult> ConfirmEmailAsync(ApiUser user, string token)
        {
            ThrowIfDisposed();
            var store = GetEmailStore();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (!await VerifyUserTokenAsync(user, CustomTokenOptions.ApiCustomEmailConfirmationTokenProvider, ConfirmEmailTokenPurpose, token))
            {
                return IdentityResult.Failed(ErrorDescriber.InvalidToken());
            }
            await store.SetEmailConfirmedAsync(user, true, CancellationToken);
            return await UpdateUserAsync(user);
        }

        private IUserEmailStore<ApiUser> GetEmailStore(bool throwOnFail = true)
        {
            var cast = Store as IUserEmailStore<ApiUser>;
            if (throwOnFail && cast == null)
            {
                throw new NotSupportedException("StoreNotIUserEmailStore");
            }
            return cast;
        }
        #endregion

        #region Generate Password Reset Token
        public override Task<string> GeneratePasswordResetTokenAsync(ApiUser user)
        {
            ThrowIfDisposed();
            return GenerateUserTokenAsync(user, CustomTokenOptions.ApiCustomResetPasswordTokenProvider, ResetPasswordTokenPurpose);
        } 
        #endregion

        #region Reset Password
        public override async Task<IdentityResult> ResetPasswordAsync(ApiUser user, string token, string newPassword)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Make sure the token is valid and the stamp matches
            if (!await VerifyUserTokenAsync(user, CustomTokenOptions.ApiCustomResetPasswordTokenProvider, ResetPasswordTokenPurpose, token))
            {
                return IdentityResult.Failed(ErrorDescriber.InvalidToken());
            }
            var result = await UpdatePasswordHash(user, newPassword, validatePassword: true);
            if (!result.Succeeded)
            {
                return result;
            }
            return await UpdateUserAsync(user);
        } 
        #endregion
    }
}
