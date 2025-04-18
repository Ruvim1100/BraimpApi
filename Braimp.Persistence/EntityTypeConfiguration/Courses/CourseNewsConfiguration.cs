using Braimp.Domain.Entities.Courses;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses;
public class CourseNewsConfiguration : IEntityTypeConfiguration<CourseNews>
{
    public void Configure(EntityTypeBuilder<CourseNews> builder)
    {
        builder.ToTable(TableNames.News);

        builder.HasKey(courseNews => courseNews.Id);

        builder.Property(courseNews => courseNews.Title)
            .HasMaxLength(100);

        builder.Property(courseNews => courseNews.Content)
            .HasMaxLength(1000);

        builder.Property(courseNews => courseNews.ImageUrl)
            .HasMaxLength(2048);

        builder.HasOne(courseNews => courseNews.Course)
            .WithMany(course => course.News)
            .HasForeignKey(courseNews => courseNews.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
