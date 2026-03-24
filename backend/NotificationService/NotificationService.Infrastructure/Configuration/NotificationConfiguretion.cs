using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationService.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Configuration
{
    public class NotificationConfiguretion : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notification");

            builder.HasKey(q => q.Id);

            builder.Property(q => q.ResipientId).IsRequired(false);

            builder.Property(q => q.Channel).HasConversion<string>().IsRequired();

            builder.Property(q => q.Subject).IsRequired();

            builder.Property(q => q.Body).IsRequired();

            builder.Property(q => q.Status).HasConversion<string>().IsRequired();

            builder.Property(q => q.SourseEvent).IsRequired();

            builder.Property(q => q.SourseEntityId).IsRequired(false);

            builder.Property(q => q.CreateAt).IsRequired();

            builder.Property(q => q.SentAt).IsRequired(false);

            builder.Property(q => q.Error).IsRequired(false);

            builder.Property(q => q.IsRead).IsRequired();


        }
    }
}
