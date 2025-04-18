using Braimp.Domain.Entities.Assignments;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Assignments
{
    public class SubmissionAttachmentConfiguration : IEntityTypeConfiguration<SubmissionAttachment>
    {
        public void Configure(EntityTypeBuilder<SubmissionAttachment> builder)
        {
            builder.ToTable(TableNames.SubmissionAttachments);

            builder.HasKey(submissionAttachment => submissionAttachment.Id);

            builder.Property(submissionAttachment => submissionAttachment.FileUrl)
                .HasMaxLength(2048);

            builder.HasOne(submissionAttachment => submissionAttachment.Submission)
                .WithMany(submission => submission.SubmissionAttachments)
                .HasForeignKey(submissionAttachment => submissionAttachment.SubmissionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
