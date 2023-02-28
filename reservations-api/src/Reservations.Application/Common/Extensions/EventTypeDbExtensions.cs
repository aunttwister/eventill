using Microsoft.EntityFrameworkCore;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Extensions
{
    public static class EventTypeDbExtensions
    {
        public static async Task<bool> EventTypeExistsAsync(
            this DbSet<EventType> eventTypes,
            EventType eventType,
            CancellationToken cancellationToken = default)
        {
            return await eventTypes
                .Where(et => et.Name == eventType.Name)
                .CountAsync(cancellationToken) == 1;
        }
    }
}
