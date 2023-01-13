using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            //services.AddIdentityCore<ApiUser>(options =>
            //{
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //});

            //services.AddScoped<IUserStore<ApiUser>, ApiUserStore>();
            //services.AddScoped<ApiUserManager>();

            services.AddIdentity<ApiUser, ApiRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<UsingIdentityWithApiContext>()
            .AddDefaultTokenProviders();
        }

        public static void AddIdentityForAspNetIdentityUser(this IServiceCollection services)
        {
            services.AddIdentity<AspUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<UsingIdentityWithApiContext>()
            .AddDefaultTokenProviders();

            //services.AddIdentityCore<AspUser>(options =>
            //{
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //});

            //services.AddScoped<IUserStore<AspUser>, UserOnlyStore<AspUser, UsingIdentityWithApiContext>>();
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
