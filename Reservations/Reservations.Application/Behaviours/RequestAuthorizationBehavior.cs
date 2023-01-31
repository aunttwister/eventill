using MediatR;
using Reservations.Application.Authorization;
using Reservations.Application.Exceptions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.Behaviours
{
    public class RequestAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestAuthorizer<TRequest> _requestAuthorizer;

        public RequestAuthorizationBehavior(IRequestAuthorizer<TRequest> requestAuthorizer)
        {
            _requestAuthorizer = requestAuthorizer;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizationResult = await _requestAuthorizer.EvaluateAuthorizationAsync(request, cancellationToken);
            if (!authorizationResult.Success)
            {
                throw new UnauthorizedException(JsonSerializer.Serialize(authorizationResult.Errors));
            }

            return await next();
        }
    }
}
