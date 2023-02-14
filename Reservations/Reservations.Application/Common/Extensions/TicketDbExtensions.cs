using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Helpers;
using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Extensions
{
    public static class TicketDbExtensions
    {
        public static async Task<bool> TicketsCountExceedsAsync(
            this DbSet<Ticket> tickets,
            decimal ticketCount,
            long eventOccurrenceId,
            CancellationToken cancellationToken = default)
        {
            return await tickets
                .Where(t => t.EventOccurenceId == eventOccurrenceId)
                .CountAsync(cancellationToken) <= ticketCount == true;
        }

        public static async Task ResetTicketStateAsync(
            this DbSet<Ticket> tickets,
            Reservation reservation,
            TicketState ticketState,
            CancellationToken cancellationToken = default)
        {
            if (!reservation.Tickets.Any())
                throw new NotFoundException(nameof(Ticket), "multiple");

            List<Ticket> updateTickets = await tickets
                .Where(t => reservation.Tickets.Select(tr => tr.Id).Contains(t.Id))
                .ToListAsync(cancellationToken);

            switch (ticketState)
            {
                case TicketState.Available:
                    reservation.Tickets.Clear();
                    break;
                case TicketState.Reserved:
                    break;
                case TicketState.Unavailable:
                    break;
                case TicketState.Sold:
                    reservation.Tickets.ForEach(t => t.TicketState = ticketState);
                    break;
                default:
                    break;
            }

            updateTickets.ForEach(t => t.TicketState = ticketState);
        }
    }
}
