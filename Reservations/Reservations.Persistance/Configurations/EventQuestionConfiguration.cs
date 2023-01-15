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
    public class EventQuestionConfiguration : AuditableEntityConfiguration<EventQuestion>
    {
        public override void ConfigureAuditableEntity(EntityTypeBuilder<EventQuestion> builder)
        {
            builder.HasKey(eq => eq.Id);
            builder.Property(eq => eq.Active)
                .HasColumnType("tinyint")
                .IsRequired();
            builder.Property(eq => eq.QuestionId)
                .IsRequired();
            builder.HasOne(eq => eq.Event)
                   .WithMany()
                   .HasForeignKey(eq => eq.EventId)
                   .IsRequired();
            builder.HasOne(eq => eq.Question)
                   .WithMany()
                   .HasForeignKey(eq => eq.QuestionId)
                   .IsRequired();
        }
    }
}
