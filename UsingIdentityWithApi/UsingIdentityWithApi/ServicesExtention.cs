using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsingIdentityWithApi.Application;
using UsingIdentityWithApi.Data;
using UsingIdentityWithApi.Logic;
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
            });

            services.AddScoped<IUserStore<ApiUser>, ApiUserStore>();
            services.AddScoped<ApiUserManager>();
        }

        public static void AddIdentityForAspNetIdentityUser(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<UsingIdentityWithApiContext>()
            .AddDefaultTokenProviders();
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
    }
}
