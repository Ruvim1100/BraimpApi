using Braimp.Domain.Entities.Assignments;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Assignments;
public class SubmissionFileConfiguration : IEntityTypeConfiguration<SubmissionFile>
{
    public void Configure(EntityTypeBuilder<SubmissionFile> builder)
    {
        builder.ToTable(TableNames.SubmissionFiles);

        builder.HasKey(submissionFile => submissionFile.Id);

        builder.HasOne(submissionFile => submissionFile.Submission)
            .WithMany(submission => submission.SubmissionFiles)
            .HasForeignKey(submissionFile => submissionFile.SubmissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
