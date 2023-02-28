using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Reservations.Commands.DeleteReservation
{
    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
    {
        private readonly IReservationDbContext _dbContext;

        public DeleteReservationCommandHandler(IReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _dbContext.Reservations
                .FirstOrDefaultAsync(e => e.Id == request.Id && !e.IsDeleted, cancellationToken);
            if (reservation == null)
            {
                throw new NotFoundException(nameof(Reservation), request.Id);
            }

            reservation.IsDeleted = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
