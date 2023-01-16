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
            })
            //.AddEntityFrameworkStores<UsingIdentityWithApiContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserStore<ApiUser>, ApiUserStore>();
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
            })
            .AddEntityFrameworkStores<UsingIdentityWithApiContext>()
            .AddDefaultTokenProviders();

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

        public static void AddJwtBearerAuthentication(this IServiceCollection services, string validIssuer,string validAudience,string issuerSigningKey)
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

        public static void AddMesc(this IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromHours(3));
        }
    }
}
