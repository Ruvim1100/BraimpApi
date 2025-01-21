using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class CourseTagConfiguration : IEntityTypeConfiguration<CourseTag>
    {
        public void Configure(EntityTypeBuilder<CourseTag> builder)
        {
            builder.ToTable("CourseTags");

            builder.HasKey(ct => ct.Id);

            builder.HasOne(ct => ct.Course)
                .WithMany(c => c.Tags)
                .HasForeignKey(ct => ct.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ct => ct.Tag)
                .WithMany(t => t.Courses)
                .HasForeignKey(ct => ct.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
