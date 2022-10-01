using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Application.services.Authentication;
using BuberDinner.InfraStructure.Authentication;
using BuberDinner.InfraStructure.Persistence;
using BuberDinner.InfraStructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.InfraStructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
