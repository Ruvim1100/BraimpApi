using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    internal class QuizResultConfiguration : IEntityTypeConfiguration<QuizResult>
    {
        public void Configure(EntityTypeBuilder<QuizResult> builder)
        {
            builder.ToTable("QuizResults");

            builder.HasKey(qr => qr.Id);

            builder.Property(qr => qr.StudentId)
                .IsRequired();

            builder.Property(qr => qr.Score)
                .IsRequired();

            builder.Property(qr => qr.Grade)
                .IsRequired(false);

            builder.Property(qr => qr.CorrectAnswerCount)
                .IsRequired();

            builder.Property(qr => qr.IncorrectAnswerCount)
                .IsRequired();

            builder.Property(qr => qr.CompletedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(qr => qr.IsVisibleToStudent)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(qr => qr.AttemptNumber)
                .IsRequired();

            builder.HasOne(qr => qr.Quiz)
                .WithMany(q => q.QuizResults)
                .HasForeignKey(qr => qr.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
