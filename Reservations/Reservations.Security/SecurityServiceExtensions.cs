using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Security.Common.Interfaces;
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
        public static void AddSecurityExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Common.Interfaces.IAuthenticationService, AuthenticationService>();
            services.AddTransient<ITokenProviderService, TokenProviderService>();
        }
    }
}
