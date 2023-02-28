using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Helpers;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Users.Commands.CreateGuestUser;
using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Reservations.Commands.ConfirmPaymentCompleted
{
    public class ConfirmPaymentCompletedCommandHandler : IRequestHandler<ConfirmPaymentCompletedCommand>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ConfirmPaymentCompletedCommandHandler(IReservationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(ConfirmPaymentCompletedCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<Reservation> requestedReservations = _mapper.Map<List<Reservation>>(request.Reservations);
            List<Reservation> reservations = await _dbContext.Reservations
                .Where(r => requestedReservations.Select(r => r.Id).Contains(r.Id)).ToListAsync();

            var reservationNotFound = request.Reservations
                .Select(r => r.Id)
                .Except(reservations
                    .Select(r => r.Id));

            if (reservationNotFound != null)
                throw new NotFoundException(nameof(Reservation), reservationNotFound.First());

            reservations.ForEach(r =>
            {
                r.PaymentCompleted = true;
                r.Tickets.ForEach(t => t.TicketState = TicketState.Sold);
            });

            _dbContext.Reservations.AddRange(reservations);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
