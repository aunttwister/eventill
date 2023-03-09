using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.Behaviours
{
    public class RequestLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public RequestLoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            _logger.LogInformation("Starting processing request {Name}", name);
            _logger.LogDebug("Request data passed: {@Request}", request);
            var response = await next();
            _logger.LogInformation("Processing request {Name} finished", name);

            return response;
        }
    }
}
