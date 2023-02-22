using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Helpers;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.EventOccurrences.Commands.EditMultipleEventOccurrences
{
    public class EditMultipleEventOccurrencesCommandHandler : IRequestHandler<EditMultipleEventOccurrencesCommand>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITicketService _ticketService;
        public EditMultipleEventOccurrencesCommandHandler(
            IReservationDbContext dbContext,
            IMapper mapper,
            ITicketService ticketService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _ticketService = ticketService;
        }

        public async Task<Unit> Handle(EditMultipleEventOccurrencesCommand request, CancellationToken cancellationToken)
        {
            var eventOccurrences = await _dbContext.EventOccurrences.AsNoTracking()
                .Include(eo => eo.Tickets).Include(eo => eo.Reservations)
                .Where(eo => request.EventOccurrences
                    .Select(reo => reo.Id)
                    .Contains(eo.Id)).ToListAsync(cancellationToken);

            if (eventOccurrences.Count() == 0)
                throw new NotFoundException(nameof(EventOccurrence), "multiple");

            _mapper.Map(request.EventOccurrences, eventOccurrences);

            foreach (var eventOccurrence in eventOccurrences)
            {
                if (!eventOccurrence.IsActive)
                {
                    _ticketService.ResetTicketState(
                        eventOccurrence.Tickets.ToList(),
                        TicketState.Unavailable);

                    eventOccurrence.Reservations.ForEach(r => r.IsDeleted = true);

                    foreach (var reservation in eventOccurrence.Reservations)
                    {
                        _dbContext.UpdateEntityState(reservation, EntityState.Modified);
                    }
                }

                else if (eventOccurrence.IsActive 
                      && eventOccurrence.Tickets.Any(t => t.TicketState == TicketState.Unavailable))
                    _ticketService.ResetTicketState(
                        eventOccurrence.Tickets.ToList(),
                        TicketState.Available);

                foreach (var ticket in eventOccurrence.Tickets)
                {
                    _dbContext.UpdateEntityState(ticket, EntityState.Modified);
                }

                _dbContext.UpdateEntityState(eventOccurrence, EntityState.Modified);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
