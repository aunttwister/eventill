using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Persistance.Configurations
{
    public abstract class AuditableEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : AuditableEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(a => a.CreatedBy)
                .HasMaxLength(128);
            builder.Property(a => a.LastModifiedBy)
                .HasMaxLength(128);
            builder.Property(a => a.Created)
                .HasColumnType("datetime")
                .HasDefaultValueSql("now()")
                .IsRequired();
            builder.Property(a => a.LastModified)
                .HasColumnType("datetime")
                .IsRequired(false);
            builder.Property(a => a.IsDeleted)
                .HasColumnType("bit")
                .HasDefaultValue(false)
                .IsRequired();
            ConfigureAuditableEntity(builder);
        }

        /// <summary>
        /// Configures model entity
        /// </summary>
        /// <param name="builder"></param>
        public abstract void ConfigureAuditableEntity(EntityTypeBuilder<TEntity> builder);
    }
}
