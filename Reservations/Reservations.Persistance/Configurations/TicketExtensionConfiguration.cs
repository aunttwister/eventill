using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Persistance.Configurations
{
    public class TicketExtensionConfiguration : AuditableEntityConfiguration<TicketExtension>
    {
        public override void ConfigureAuditableEntity(EntityTypeBuilder<TicketExtension> builder)
        {
            builder.HasKey(te => te.Id);
            builder.HasOne(te => te.Ticket)
                .WithMany(t => t.TicketExtensions)
                .HasForeignKey(te => te.TicketId)
                .IsRequired();
            builder.HasOne(te => te.Extension)
                .WithMany(t => t.TicketExtensions)
                .HasForeignKey(te => te.ExtensionId)
                .IsRequired();
        }
    }
}
