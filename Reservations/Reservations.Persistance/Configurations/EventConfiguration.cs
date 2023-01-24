using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Persistance.Configurations
{
    public class EventConfiguration : AuditableEntityConfiguration<Event>
    {
        public override void ConfigureAuditableEntity(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Description)
                .HasMaxLength(255)
                .IsRequired(); ;
            builder.Property(e => e.Length)
                .HasColumnType("bigint")
                .IsRequired();
            builder.HasOne(e => e.EventType)
                .WithMany(et => et.Events)
                .HasForeignKey(e => e.EventTypeId)
                .IsRequired();
            builder.HasMany(e => e.EventOccurrences)
                .WithOne(eo => eo.Event)
                .IsRequired();
            builder.HasMany(e => e.Questions)
                .WithMany(q => q.Events)
                .UsingEntity<EventQuestion>();
        }
    }
}
