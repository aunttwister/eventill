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
    public class ReservationConfiguration : AuditableEntityConfiguration<Reservation>
    {
        public override void ConfigureAuditableEntity(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.Id);
            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reservations)
                   .HasForeignKey(r => r.UserId)
                   .IsRequired();
            builder.HasMany(r => r.Tickets)
                   .WithOne(t => t.Reservation)
                   .IsRequired();
        }
    }
}
