using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.Common.Services;
using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Reservations.Commands.EditMultipleReservation
{
    public class EditMultipleReservationsCommandHandler : IRequestHandler<EditMultipleReservationsCommand>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;
        public EditMultipleReservationsCommandHandler(
            IReservationDbContext dbContext, 
            IMapper mapper,
            ITicketService ticketService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _ticketService = ticketService;
        }

        public async Task<Unit> Handle(EditMultipleReservationsCommand request, CancellationToken cancellationToken)
        {
            var reservations = await _dbContext.Reservations.AsNoTracking()
                .Where(r => request.Reservations
                   .Select(rr => rr.Id)
                   .Contains(r.Id)).ToListAsync(cancellationToken);

            if (reservations.Count() == 0)
                throw new NotFoundException(nameof(Reservation), "multiple");

            _mapper.Map(request.Reservations, reservations);

            foreach (var item in reservations)
            {
                if (item.IsDeleted)
                {
                    _ticketService.ResetTicketState(
                        item.Tickets.ToList(),
                        TicketState.Available);

                    item.EventOccurrence = null;
                }

                else if (item.PaymentCompleted)
                    _ticketService.ResetTicketState(
                        item.Tickets.ToList(),
                        TicketState.Sold);

                else if (!item.PaymentCompleted)
                    _ticketService.ResetTicketState(
                        item.Tickets.ToList(),
                        TicketState.Reserved);

                foreach (var ticket in item.Tickets)
                {
                    _dbContext.UpdateEntityState(ticket, EntityState.Modified);
                }

                _dbContext.UpdateEntityState(item, EntityState.Modified);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
