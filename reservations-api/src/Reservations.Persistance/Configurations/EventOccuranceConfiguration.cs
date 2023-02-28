using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Persistance.Configurations
{
    public class EventOccuranceConfiguration : AuditableEntityConfiguration<EventOccurrence>
    {
        public override void ConfigureAuditableEntity(EntityTypeBuilder<EventOccurrence> builder)
        {
            builder.HasQueryFilter(eo => !eo.IsDeleted);
            builder.HasKey(eo => eo.Id);
            builder.Property(eo => eo.StartTime)
                .IsRequired();
            builder.Property(eo => eo.IsActive)
                .HasColumnType("tinyint")
                .HasDefaultValue(false)
                .IsRequired();
            builder.HasMany(eo => eo.Reservations)
                .WithOne(r => r.EventOccurrence);
            builder.HasOne(eo => eo.Event)
                .WithMany(e => e.EventOccurrences)
                .HasForeignKey(eo => eo.EventId)
                .IsRequired();
            builder.HasMany(eo => eo.Tickets)
                .WithOne(t => t.EventOccurence)
                .IsRequired();
        }
    }
}
