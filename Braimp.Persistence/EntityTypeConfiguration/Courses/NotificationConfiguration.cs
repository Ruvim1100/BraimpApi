using Braimp.Domain.Entities.Notifications;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable(TableNames.Notifications);

            builder.HasKey(notification => notification.Id);

            builder.Property(notification => notification.Title)
                .HasMaxLength(100);

            builder.Property(notification => notification.Message)
                .HasMaxLength(300);

            builder.Property(notification => notification.IsRead)
                .HasDefaultValue(false);

            builder.HasOne(notification => notification.Course)
                .WithMany(course => course.Notifications)
                .HasForeignKey(notification => notification.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
