using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.Common.Services;
using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Reservations.Commands.EditMultipleReservation
{
    public class EditMultipleReservationsCommandHandler : IRequestHandler<EditMultipleReservationsCommand>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly IMapper _mapper;
        public EditMultipleReservationsCommandHandler(
            IReservationDbContext dbContext, 
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditMultipleReservationsCommand request, CancellationToken cancellationToken)
        {
            List<Reservation> reservations = await _dbContext.Reservations.Include(r => r.Tickets)
                .Where(r => request.Reservations.Select(rq => rq.Id).Contains(r.Id))
                .ToListAsync(cancellationToken);

            if (!await _dbContext.Reservations.ReservationsExistAsync(reservations))
                throw new NotFoundException(nameof(Reservation), "multiple");

            reservations = _mapper.Map<List<Reservation>>(request.Reservations);

            foreach (var item in reservations)
            {
                if (item.IsDeleted)
                    await _dbContext.Tickets.ResetTicketStateAsync(item, TicketState.Available, cancellationToken);

                else if (item.PaymentCompleted)
                    await _dbContext.Tickets.ResetTicketStateAsync(item, TicketState.Sold, cancellationToken);
            }

            _dbContext.Reservations.UpdateRange(reservations);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
