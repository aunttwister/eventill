using Microsoft.EntityFrameworkCore;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Extensions
{
    public static class ReservationsDbExtensions
    {
        public static async Task<bool> ReservationExistsAsync(
            this DbSet<Reservation> reservations,
            Reservation reservation,
            CancellationToken cancellationToken = default)
        {
            return await reservations
                .Where(r => r.Id == reservation.Id)
                .CountAsync(cancellationToken) == 1;
        }

        public static async Task<bool> ReservationsExistAsync(
            this DbSet<Reservation> reservations,
            IEnumerable<Reservation> multipleReservations,
            CancellationToken cancellationToken = default)
        {
            return await reservations
                .Where(r => multipleReservations.Any(mr => mr.Id == r.Id))
                .CountAsync(cancellationToken) == 1;
        }
    }
}
