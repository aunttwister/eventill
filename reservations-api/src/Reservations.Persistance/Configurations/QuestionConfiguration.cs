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
    public class QuestionConfiguration : AuditableEntityConfiguration<Question>
    {
        public override void ConfigureAuditableEntity(EntityTypeBuilder<Question> builder)
        {
            builder.HasQueryFilter(q => !q.IsDeleted);
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Content)
                .HasMaxLength(255)
                .IsRequired();
            builder.HasMany(q => q.Events)
                   .WithMany(e => e.Questions)
                   .UsingEntity<EventQuestion>();
        }
    }
}
