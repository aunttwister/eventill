using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Application;
using Reservations.Security.Common.Interfaces;
using Reservations.Security.Common.Options;
using Reservations.Security.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security
{
    public static class SecurityServiceExtensions
    {
        public static void AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(options =>
            {
                options.AsTransient();
            },
            Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(SecurityServiceExtensions).Assembly);

            services.Configure<TokenProviderOptions>(configuration.GetSection("JWTAuthentication"));

            services.AddTransient<Common.Interfaces.IAuthenticationService, AuthenticationService>();
            services.AddTransient<ITokenProviderService, TokenProviderService>();
        }
    }
}
