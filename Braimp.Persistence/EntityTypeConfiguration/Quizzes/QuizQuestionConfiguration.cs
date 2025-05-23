using Braimp.Domain.Entities.Quizzes;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes;
public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
{
    public void Configure(EntityTypeBuilder<QuizQuestion> builder)
    {
        builder.ToTable(TableNames.QuizQuestions);

        builder.HasKey(quizQuestion => quizQuestion.Id);

        builder.Property(quizQuestion => quizQuestion.Text)
            .HasMaxLength(300);

        builder.Property(quizQuestion => quizQuestion.MediaUrl)
            .HasMaxLength(2048);

        builder.Property(quizQuestion => quizQuestion.Weight)
            .HasDefaultValue(1);

        builder.Property(quizQuestion => quizQuestion.QuestionType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(quizQuestion => quizQuestion.Quiz)
            .WithMany(quiz => quiz.Questions)
            .HasForeignKey(quizQuestion => quizQuestion.QuizId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
