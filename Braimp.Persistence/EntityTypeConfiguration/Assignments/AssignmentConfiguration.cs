using Braimp.Domain.Entities.Assignments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Assignments
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable("Assignments");

            builder.HasKey(assignment => assignment.Id);

            builder.Property(assignment => assignment.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(assignment => assignment.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(assignment => assignment.AttachmentUrl)
                .IsRequired(false);

            builder.Property(assignment => assignment.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(assignment => assignment.UpdatedAt)
                .IsRequired(false);

            builder.Property(assignment => assignment.Deadline)
                .IsRequired(false);

            builder.HasOne(assignment => assignment.Course)
                .WithMany(course => course.Assignments)
                .HasForeignKey(assignment => assignment.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
