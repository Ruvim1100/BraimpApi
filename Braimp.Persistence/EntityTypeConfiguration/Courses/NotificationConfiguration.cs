using Braimp.Domain.Entities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(notification => notification.Id);

            builder.Property(notification => notification.UserId)
                .IsRequired();

            builder.Property(notification => notification.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(notification => notification.Message)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(notification => notification.IsRead)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(notification => notification.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(notification => notification.UpdatedAt)
                .IsRequired(false);

            builder.Property(notification => notification.Type)
                .IsRequired();

            builder.HasOne(notification => notification.Course)
                .WithMany(course => course.Notifications)
                .HasForeignKey(notification => notification.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
