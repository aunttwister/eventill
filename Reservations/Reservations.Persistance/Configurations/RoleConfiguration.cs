using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Persistance.Configurations
{
    public class RoleConfiguration : AuditableEntityConfiguration<Role>
    {
        public override void ConfigureAuditableEntity(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name)
                .HasMaxLength(128)
                .IsRequired();
            builder.HasMany(r => r.Users)
                   .WithOne(u => u.Role)
                   .IsRequired();

            builder.HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Guest" }
            );
        }
    }
}
