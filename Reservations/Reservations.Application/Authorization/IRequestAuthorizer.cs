using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.Authorization
{
    public interface IRequestAuthorizer<TRequest> where TRequest : notnull
    {
        Task<AuthorizationResult> EvaluateAuthorizationAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}
