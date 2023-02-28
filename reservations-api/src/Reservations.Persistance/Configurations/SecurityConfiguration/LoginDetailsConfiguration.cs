using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Persistance.Configurations.SecurityConfiguration
{
    public class LoginDetailsConfiguration : AuditableEntityConfiguration<LoginDetails>
    {
        public override void ConfigureAuditableEntity(EntityTypeBuilder<LoginDetails> builder)
        {
            builder.HasKey(ld => ld.Id);
            builder.Property(ld => ld.HashPassword)
                .HasMaxLength(20)
                .HasColumnType("binary(20)")
                .IsRequired();
            builder.Property(ld => ld.Salt)
                .HasMaxLength(16)
                .HasColumnType("binary(16)")
                .IsRequired();
            builder.HasOne(ld => ld.User)
                .WithOne(u => u.LoginDetails)
                .IsRequired();
        }
    }
}
