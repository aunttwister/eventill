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
    }
}
