using Braimp.Domain.Entities.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses
{
    public class CourseCategoryConfiguration : IEntityTypeConfiguration<CourseCategory>
    {
        public void Configure(EntityTypeBuilder<CourseCategory> builder)
        {
            builder.ToTable("CourseCategories");

            builder.HasKey(courseCategory => courseCategory.Id);

            builder.Property(courseCategory => courseCategory.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
