using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Persistance.Configurations
{
    public class TicketConfiguration : AuditableEntityConfiguration<Ticket>
    {
        public override void ConfigureAuditableEntity(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TicketState)
                .HasColumnType($"enum('{TicketState.Available}', '{TicketState.Unavailable}', '{TicketState.Reserved}', '{TicketState.Sold}')")
                .IsRequired();
            builder.Property(t => t.Price)
                .IsRequired();
            builder.HasOne(t => t.Reservation)
                .WithMany(r => r.Tickets)
                .HasForeignKey(t => t.ReservationId)
                .IsRequired(false);
            builder.HasOne(t => t.EventOccurence)
                .WithMany(r => r.Tickets)
                .HasForeignKey(t => t.EventOccurenceId)
                .IsRequired(false);
            builder.HasMany(t => t.TicketExtensions)
                .WithOne(te => te.Ticket)
                .IsRequired();
        }
    }
}
