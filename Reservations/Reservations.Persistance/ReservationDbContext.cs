using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain;
using Reservations.Persistance.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Persistance
{
    public class ReservationDbContext : DbContext, IReservationDbContext
    {
        private readonly ICurrentUserService _userService;
        private readonly IDateTime _dateTime;

        public ReservationDbContext(DbContextOptions<ReservationDbContext> options) : base(options) { }

        public ReservationDbContext(DbContextOptions<ReservationDbContext> options,
            IDateTime datetime,
            ICurrentUserService userService) : base(options)
        {
            _dateTime = datetime;
            _userService = userService;
        }

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

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //var auditEntries = await OnBeforeSaveChangesAsync();
            var result = await base.SaveChangesAsync(cancellationToken);
            //await OnAfterSaveChanges(auditEntries);

            return result;
        }

        //Not used
        private async Task<List<AuditEntry>> OnBeforeSaveChangesAsync()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Metadata.GetTableName();
                auditEntry.Action = entry.State.ToString();
                var currentUser = await _userService.GetCurrentUserAsync();
                auditEntry.UserId = currentUser?.Id.ToString();
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            entry.Entity.Created = _dateTime.UtcNow;
                            entry.Entity.CreatedBy = currentUser?.Id.ToString();
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            entry.Entity.LastModified = _dateTime.UtcNow;
                            entry.Entity.LastModifiedBy = currentUser?.Id.ToString();
                            break;
                    }
                }
            }
            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                Audits.Add(auditEntry.ToAudit());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        //Not used
        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }

                // Save the Audit entry
                Audits.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}
