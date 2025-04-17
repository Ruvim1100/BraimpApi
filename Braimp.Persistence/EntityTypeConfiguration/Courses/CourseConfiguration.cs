using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");

            builder.HasKey(course => course.Id);

            builder.Property(course => course.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(course => course.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(course => course.Status)
                .IsRequired()
                .HasDefaultValue(CourseStatus.Pending);

            builder.Property(course => course.GradingSystem)
                .IsRequired()
                .HasDefaultValue(GradingSystem.PointsOutOf10);

            builder.Property(course => course.CoverImageUrl)
                .IsRequired(false);

            builder.Property(course => course.BackgroundColor)
                .IsRequired(false);

            builder.Property(course => course.LogoUrl)
                .IsRequired(false);

        builder.Property(course => course.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(course => course.UpdatedAt)
                .IsRequired(false);

            builder.Property(course => course.OwnerId)
                .IsRequired();

            builder.HasOne(course => course.CourseCategory)
                .WithMany(courseCategory => courseCategory.Courses)
                .HasForeignKey(course => course.CourseCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
