
using Braimp.Domain.Entities;
using Braimp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(c => c.Status)
                .IsRequired()
                .HasDefaultValue(CourseStatus.Pending);

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(c => c.OwnerId)
                .IsRequired();

            builder.HasOne(c => c.CourseCategory)
                .WithMany(cc => cc.Courses)
                .HasForeignKey(c => c.CourseCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
