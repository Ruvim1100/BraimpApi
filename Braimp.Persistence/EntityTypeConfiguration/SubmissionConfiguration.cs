using Braimp.Domain.Entities;
using Braimp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.ToTable("Submissions");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Status)
                .HasDefaultValue(SubmissionStatus.Pending)
                .IsRequired();

            builder.Property(s => s.StudentId)
                .IsRequired();

            builder.Property(s => s.ReviewerId)
                .IsRequired(false);

            builder.Property(s => s.Text)
                .HasMaxLength(300)
                .IsRequired(false);

            builder.Property(s => s.Grade)
                .IsRequired(false);

            builder.Property(s => s.ReviewComment)
                .HasMaxLength(300)
                .IsRequired(false);

            builder.Property(s => s.SubmittedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(s => s.ReviewedAt)
                .IsRequired(false);

            builder.HasOne(s => s.Assignment)
                .WithMany(s => s.Submissions)
                .HasForeignKey(s => s.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
