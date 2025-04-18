using Braimp.Domain.Entities.Courses;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses
{
    public class CourseCategoryConfiguration : IEntityTypeConfiguration<CourseCategory>
    {
        public void Configure(EntityTypeBuilder<CourseCategory> builder)
        {
            builder.ToTable(TableNames.Categories);

            builder.HasKey(courseCategory => courseCategory.Id);

            builder.Property(courseCategory => courseCategory.Name)
                .HasMaxLength(100);
        }
    }
}
