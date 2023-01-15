using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Persistance.Configurations
{
    public class EventTypeConfiguration : AuditableEntityConfiguration<EventType>
    {
        public override void ConfigureAuditableEntity(EntityTypeBuilder<EventType> builder)
        {
            builder.HasKey(et => et.Id);
            builder.Property(et => et.Name)
                .HasMaxLength(128)
                .IsRequired();
            builder.HasMany(et => et.Events)
                .WithOne(e => e.EventType)
                .IsRequired();
        }
    }
}
