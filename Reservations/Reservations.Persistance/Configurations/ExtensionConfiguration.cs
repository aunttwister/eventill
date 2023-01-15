using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Persistance.Configurations
{
    public class ExtensionConfiguration : AuditableEntityConfiguration<Extension>
    {
        public override void ConfigureAuditableEntity(EntityTypeBuilder<Extension> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(e => e.Price)
                .IsRequired();
            builder.HasMany(e => e.TicketExtensions)
                .WithOne(te => te.Extension)
                .IsRequired();
        }
    }
}
