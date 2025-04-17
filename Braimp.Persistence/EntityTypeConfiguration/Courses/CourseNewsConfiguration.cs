using Braimp.Domain.Entities.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses
{
    public class CourseNewsConfiguration : IEntityTypeConfiguration<CourseNews>
    {
        public void Configure(EntityTypeBuilder<CourseNews> builder)
        {
            builder.ToTable("CourseNews");

            builder.HasKey(courseNews => courseNews.Id);

            builder.Property(courseNews => courseNews.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(courseNews => courseNews.Content)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(courseNews => courseNews.ImageUrl)
                .HasMaxLength(2048)
                .IsRequired(false);

            builder.Property(courseNews => courseNews.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(courseNews => courseNews.UpdatedAt)
                .IsRequired(false);

            builder.Property(courseNews => courseNews.AuthorId)
                .IsRequired();

            builder.HasOne(courseNews => courseNews.Course)
                .WithMany(course => course.News)
                .HasForeignKey(courseNews => courseNews.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
