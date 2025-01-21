using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(n => n.Id);

            builder.Property(n => n.UserId)
                .IsRequired();

            builder.Property(n => n.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(n => n.Message)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(n => n.IsRead)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(n => n.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(n => n.Type)
                .IsRequired();

            builder.HasOne(n => n .Course)
                .WithMany(c => c.Notifications)
                .HasForeignKey(n => n.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
