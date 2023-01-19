using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddTransient<IEventSetupService, EventSetupService>();
        }
    }
}
