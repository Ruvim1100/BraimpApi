using Braimp.Domain.Entities.Quizzes;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes;
internal class QuizResultConfiguration : IEntityTypeConfiguration<QuizResult>
{
    public void Configure(EntityTypeBuilder<QuizResult> builder)
    {
        builder.ToTable(TableNames.QuizResults);

        builder.HasKey(quizResult => quizResult.Id);

        builder.Property(quizResult => quizResult.Score)
            .HasPrecision(5, 2);

        builder.Property(quizResult => quizResult.Grade)
            .HasPrecision(5, 2);

        builder.Property(quizResult => quizResult.IsVisibleToStudent)
            .HasDefaultValue(true);

        builder.HasOne(quizResult => quizResult.Quiz)
            .WithMany(quiz => quiz.QuizResults)
            .HasForeignKey(quizResult => quizResult.QuizId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
