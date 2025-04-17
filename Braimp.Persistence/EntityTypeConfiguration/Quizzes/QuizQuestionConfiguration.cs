using Braimp.Domain.Entities.Quizzes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes
{
    public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.ToTable("QuizQuestions");

            builder.HasKey(quizQuestion => quizQuestion.Id);

            builder.Property(quizQuestion => quizQuestion.Text)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(quizQuestion => quizQuestion.QuestionType)
                .IsRequired();

            builder.Property(quizQuestion => quizQuestion.MediaUrl)
                .HasMaxLength(2048)
                .IsRequired(false);

            builder.Property(quizQuestion => quizQuestion.Weight)
                .HasDefaultValue(1)
                .IsRequired();

            builder.HasOne(quizQuestion => quizQuestion.Quiz)
                .WithMany(quiz => quiz.Questions)
                .HasForeignKey(quizQuestion => quizQuestion.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
