using Microsoft.EntityFrameworkCore;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Extensions
{
    public static class EventOccurrenceDbExtensions
    {
        public static async Task<IEnumerable<EventOccurrence>> ReturnEventOccurrenceDeltaAsync(
            this DbSet<EventOccurrence> eventOccurrences,
            IEnumerable<EventOccurrence> newEventOccurrences,
            CancellationToken cancellationToken = default)
        {
            if (!eventOccurrences.Any())
                return newEventOccurrences;
            return await Task.Run(() => newEventOccurrences.Except(eventOccurrences), cancellationToken);
        }

        public static async Task<bool> EventOccurrenceExistsAsync(
            this DbSet<EventOccurrence> eventOccurences,
            EventOccurrence eventOccurence,
            CancellationToken cancellationToken = default)
        {
            return await eventOccurences
                .Where(eo => eo.StartTime == eventOccurence.StartTime)
                .CountAsync(cancellationToken) == 1;
        }
    }
}
