using Braimp.Domain.Entities.Assignments;
using Braimp.Domain.Entities.Assignments.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Assignments
{
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.ToTable("Submissions");

            builder.HasKey(submission => submission.Id);

            builder.Property(submission => submission.Status)
                .HasDefaultValue(SubmissionStatus.Pending)
                .IsRequired();

            builder.Property(submission => submission.StudentId)
                .IsRequired();

            builder.Property(submission => submission.ReviewerId)
                .IsRequired(false);

            builder.Property(submission => submission.Text)
                .HasMaxLength(300)
                .IsRequired(false);

            builder.Property(submission => submission.Grade)
                .HasPrecision(5, 2)
                .IsRequired(false);

            builder.Property(submission => submission.ReviewComment)
                .HasMaxLength(300)
                .IsRequired(false);

            builder.Property(submission => submission.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(submission => submission.UpdatedAt)
                .IsRequired(false);

            builder.Property(submission => submission.ReviewedAt)
                .IsRequired(false);

            builder.HasOne(submission => submission.Assignment)
                .WithMany(assignment => assignment.Submissions)
                .HasForeignKey(submission => submission.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
