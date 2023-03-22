using Reservations.Application.Authorization;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.Reservations.Commands.EditMultipleReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.EventOccurrences.Commands.EditMultipleReservations
{
    public class EditMultipleReservationsCommandAuthorizer : IRequestAuthorizer<EditMultipleReservationsCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        public EditMultipleReservationsCommandAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<AuthorizationResult> EvaluateAuthorizationAsync(EditMultipleReservationsCommand request, CancellationToken cancellationToken = default)
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
