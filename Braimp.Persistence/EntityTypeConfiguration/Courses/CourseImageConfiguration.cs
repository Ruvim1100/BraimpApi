using Braimp.Domain.Entities.Courses;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses;
public class CourseImageConfiguration : IEntityTypeConfiguration<CourseImage>
{
    public void Configure(EntityTypeBuilder<CourseImage> builder)
    {
        builder.ToTable(TableNames.CourseImages);

        builder.HasKey(image => image.Id);

        builder.HasOne(courseImage => courseImage.Course)
            .WithOne(course => course.Image)
            .HasForeignKey<CourseImage>(courseImage => courseImage.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
