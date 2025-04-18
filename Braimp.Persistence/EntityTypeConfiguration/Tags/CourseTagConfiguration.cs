using Braimp.Domain.Entities.Tags;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Tags
{
    public class CourseTagConfiguration : IEntityTypeConfiguration<CourseTag>
    {
        public void Configure(EntityTypeBuilder<CourseTag> builder)
        {
            builder.ToTable(TableNames.CourseTags);

            builder.HasKey(courseTag => courseTag.Id);

            builder.HasOne(courseTag => courseTag.Course)
                .WithMany(course => course.Tags)
                .HasForeignKey(courseTag => courseTag.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(courseTag => courseTag.Tag)
                .WithMany(tag => tag.Courses)
                .HasForeignKey(courseTag => courseTag.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
