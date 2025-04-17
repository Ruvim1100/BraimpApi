using Braimp.Domain.Entities.Quizzes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes
{
    internal class QuizResultConfiguration : IEntityTypeConfiguration<QuizResult>
    {
        public void Configure(EntityTypeBuilder<QuizResult> builder)
        {
            builder.ToTable("QuizResults");

            builder.HasKey(quizResult => quizResult.Id);

            builder.Property(quizResult => quizResult.StudentId)
                .IsRequired();

            builder.Property(quizResult => quizResult.Score)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(quizResult => quizResult.Grade)
                .HasPrecision(5, 2)
                .IsRequired(false);

            builder.Property(quizResult => quizResult.CorrectAnswerCount)
                .IsRequired();

            builder.Property(quizResult => quizResult.IncorrectAnswerCount)
                .IsRequired();

            builder.Property(quizResult => quizResult.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(quizResult => quizResult.UpdatedAt)
                .IsRequired(false);

            builder.Property(quizResult => quizResult.IsVisibleToStudent)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(quizResult => quizResult.AttemptNumber)
                .IsRequired();

            builder.HasOne(quizResult => quizResult.Quiz)
                .WithMany(quiz => quiz.QuizResults)
                .HasForeignKey(quizResult => quizResult.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
