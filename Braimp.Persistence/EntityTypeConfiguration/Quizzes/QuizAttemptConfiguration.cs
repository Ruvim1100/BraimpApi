using Braimp.Domain.Entities.Quizzes;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes;
internal class QuizAttemptConfiguration : IEntityTypeConfiguration<QuizAttempt>
{
    public void Configure(EntityTypeBuilder<QuizAttempt> builder)
    {
        builder.ToTable(TableNames.QuizResults);

        builder.HasKey(quizAttempt => quizAttempt.Id);

        builder.Property(quizAttempt => quizAttempt.Score)
            .HasPrecision(5, 2);

        builder.Property(quizAttempt => quizAttempt.Grade)
            .HasPrecision(5, 2);

        builder.Property(quizAttempt => quizAttempt.IsPublished)
            .HasDefaultValue(true);

        builder.HasOne(quizAttempt => quizAttempt.Quiz)
            .WithMany(quiz => quiz.QuizAttempts)
            .HasForeignKey(quizAttempt => quizAttempt.QuizId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
