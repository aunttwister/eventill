using Microsoft.EntityFrameworkCore;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Interfaces
{
    public interface IReservationDbContext : IDisposable
    {
        public DbSet<Audit> Audits { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LoginDetails> LoginDetails { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<EventQuestion> EventQuestions { get; set; }
        public DbSet<EventOccurrence> EventOccurrences { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
