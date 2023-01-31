using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Application.Authorization;
using Reservations.Application.Behaviours;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.Common.Services;
using System.Linq;
using System.Reflection;

namespace Reservations.Application
{
    public static class ApplicationServiceExtensions
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(options =>
            {
                options.AsTransient();
            },
            Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(ApplicationServiceExtensions).Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestAuthorizationBehavior<,>));

            services.AddTransient<IEventSetupService, EventSetupService>();

            AddAuthorizers(services);
        }

        private static void AddAuthorizers(IServiceCollection services)
        {
            services.AddTransient(typeof(IRequestAuthorizer<>), typeof(BaseRequestAuthorizer<>));
            var authorizerInterface = typeof(IRequestAuthorizer<>);
            var authorizers = authorizerInterface.Assembly.GetTypes()
                .Where(t => !t.IsAbstract
                    && t != typeof(BaseRequestAuthorizer<>)
                    && (authorizerInterface.IsAssignableFrom(t)
                    || t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == authorizerInterface)))
                .ToList();
            if (authorizers != null && authorizers.Count > 0)
            {
                foreach (var authorizer in authorizers)
                {
                    var specifiedInterface = authorizer.GetInterfaces()
                        .FirstOrDefault(i => i.GetGenericTypeDefinition() == authorizerInterface);
                    if (specifiedInterface != null)
                    {
                        services.AddTransient(specifiedInterface, authorizer);
                    }
                }
            }
        }
    }
}
