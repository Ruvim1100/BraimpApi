using Braimp.Domain.Entities.Assignments;
using Braimp.Domain.Entities.Assignments.Enums;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Assignments;
public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        builder.ToTable(TableNames.Submissions);

        builder.HasKey(submission => submission.Id);

        builder.Property(submission => submission.Status)
            .HasConversion<string>()
            .HasDefaultValue(SubmissionStatus.Pending);

        builder.Property(submission => submission.Text)
            .HasMaxLength(300);

        builder.Property(submission => submission.Grade)
            .HasPrecision(5, 2);

        builder.Property(submission => submission.ReviewComment)
            .HasMaxLength(300);

        builder.HasOne(submission => submission.Assignment)
            .WithMany(assignment => assignment.Submissions)
            .HasForeignKey(submission => submission.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
