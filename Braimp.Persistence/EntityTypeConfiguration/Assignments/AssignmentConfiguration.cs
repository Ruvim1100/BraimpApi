using Braimp.Domain.Entities.Assignments;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Assignments
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable(TableNames.Assignments);

            builder.HasKey(assignment => assignment.Id);

            builder.Property(assignment => assignment.Title)
                .HasMaxLength(100);

            builder.Property(assignment => assignment.Description)
                .HasMaxLength(500);

            builder.HasOne(assignment => assignment.Course)
                .WithMany(course => course.Assignments)
                .HasForeignKey(assignment => assignment.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
