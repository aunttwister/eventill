using Reservations.Application.Authorization;
using Reservations.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.EventOccurrences.Commands.EditMultipleEventOccurrences
{
    public class EditMultipleEventOccurrencesCommandAuthorizer : IRequestAuthorizer<EditMultipleEventOccurrencesCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        public EditMultipleEventOccurrencesCommandAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<AuthorizationResult> EvaluateAuthorizationAsync(EditMultipleEventOccurrencesCommand request, CancellationToken cancellationToken = default)
        {
            var user = await _currentUserService.GetCurrentUserAsync();

            if (user != null && user.Role == "Admin")
            {
                return new AuthorizationResult(true);
            }

            return new AuthorizationResult(
                success: false,
                errors: new List<string> { "User is not authorized to update reservations." }
            );
        }
    }
}
