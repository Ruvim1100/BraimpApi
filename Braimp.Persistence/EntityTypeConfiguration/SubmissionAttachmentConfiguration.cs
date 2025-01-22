using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class SubmissionAttachmentConfiguration : IEntityTypeConfiguration<SubmissionAttachment>
    {
        public void Configure(EntityTypeBuilder<SubmissionAttachment> builder)
        {
            builder.ToTable("SubmissionAttachments");

            builder.HasKey(sa => sa.Id);

            builder.Property(sa => sa.FileUrl)
                .HasMaxLength(2048)
                .IsRequired();

            builder.HasOne(sa => sa.Submission)
                .WithMany(s => s.SubmissionAttachments)
                .HasForeignKey(sa => sa.SubmissionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
