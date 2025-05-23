using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses;
public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable(TableNames.Courses);

        builder.HasKey(course => course.Id);

        builder.Property(course => course.Title)
            .HasMaxLength(100);

        builder.Property(course => course.Description)
            .HasMaxLength(1000);

        builder.Property(course => course.Status)
            .HasConversion<string>()
            .HasDefaultValue(CourseStatus.Pending)
            .HasMaxLength(50);

        builder.Property(course => course.GradingSystem)
            .HasConversion<string>()
            .HasDefaultValue(GradingSystem.PointsOutOf10)
            .HasMaxLength(50);

        builder.Property(course => course.CoverImageUrl)
            .HasMaxLength(2048);

        builder.Property(course => course.BackgroundColor)
            .HasMaxLength(50);

        builder.Property(course => course.LogoUrl)
            .HasMaxLength(2048);

        builder.HasOne(course => course.CourseCategory)
            .WithMany(courseCategory => courseCategory.Courses)
            .HasForeignKey(course => course.CourseCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
