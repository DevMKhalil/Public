using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsingIdentityWithApi.Application;
using UsingIdentityWithApi.Data;
using UsingIdentityWithApi.Logic.api;
using UsingIdentityWithApi.Logic.asp;
using UsingIdentityWithApi.Logic.Shared;
using static CSharpFunctionalExtensions.Result;

namespace UsingIdentityWithApi
{
    public static class ServicesExtention
    {
        public static void AddIdentityForIdentityUser(this IServiceCollection services)
        {
            services.AddIdentityCore<ApiUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Tokens.PasswordResetTokenProvider = "ApiCustomeResetPasswordTokenProvider";
                options.Tokens.EmailConfirmationTokenProvider = "ApiCustomEmailConfirmationTokenProvider";

            })
            .AddTokenProvider<CustomApiPasswordResetTokenProvider<ApiUser>>("ApiCustomeResetPasswordTokenProvider")
            .AddTokenProvider<CustomApiEmailConfirmationTokenProvider<ApiUser>>("ApiCustomEmailConfirmationTokenProvider")
            .AddPasswordValidator<CustomePasswordValidator<ApiUser>>()
            ;

            services.AddScoped<IUserStore<ApiUser>, ApiUserStore>();
            services.AddScoped<IUserTwoFactorTokenProvider<ApiUser>, CustomDataProtectionTokenProvider<ApiUser>>();
            services.AddScoped<ApiUserManager>();

            services.AddScoped<IUserClaimsPrincipalFactory<ApiUser>, ApiUserClaimsPrincipalFactory>();
        }

        public static void AddIdentityForAspNetIdentityUser(this IServiceCollection services)
        {
            services.AddIdentity<AspUser, IdentityRole<string>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;

                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Tokens.PasswordResetTokenProvider = "CustomeAspResetPasswordTokenProvider";
                options.Tokens.EmailConfirmationTokenProvider = "CustomAspEmailConfirmationTokenProvider";
            })
            .AddEntityFrameworkStores<UsingIdentityWithApiContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<CustomAspPasswordResetTokenProvider<AspUser>>("CustomeAspResetPasswordTokenProvider")
            .AddTokenProvider<CustomAspEmailConfirmationTokenProvider<AspUser>>("CustomAspEmailConfirmationTokenProvider")
            .AddPasswordValidator<CustomePasswordValidator<AspUser>>()
            ;

            services.AddScoped<IUserClaimsPrincipalFactory<AspUser>, AspUserClaimsPrincipalFactory>();
        }

        public static void AddContext(this IServiceCollection services, string connectionString)
        {
            ILoggerFactory ConsolLoggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddFilter(DbLoggerCategory.Database.Transaction.Name, LogLevel.Debug);
                builder.AddFilter(DbLoggerCategory.Database.Connection.Name, LogLevel.Information);
            });

            services.AddDbContext<UsingIdentityWithApiContext>(options =>
            {
                options
                .UseLoggerFactory(ConsolLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer(connectionString);

            });

            services.AddTransient<IUsingIdentityWithApiContext, UsingIdentityWithApiContext>();
        }

        public static void AddJwtBearerAuthentication(this IServiceCollection services, string validIssuer, string validAudience, string issuerSigningKey)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = validIssuer,
                    ValidAudience = validAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey))
                };
            });
        }

        public static void AddMisc(this IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(3));
        }
    }
}
