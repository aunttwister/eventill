using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Persistance.Configurations.SecurityConfiguration
{
    public class UserConfiguration : AuditableEntityConfiguration<User>
    {
        public override void ConfigureAuditableEntity(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email)
                .HasMaxLength(128)
                .IsRequired();
            builder.Property(u => u.Name)
                .IsRequired();
            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired(false);;
            builder.Property(u => u.RoleId)
                .IsRequired();
            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);
            builder.HasOne(u => u.LoginDetails)
                .WithOne(ld => ld.User)
                .HasForeignKey<LoginDetails>(u => u.UserId)
                .IsRequired();
        }
    }
}
