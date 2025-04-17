using Braimp.Domain.Entities.Quizzes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes
{
    public class QuizOptionConfiguration : IEntityTypeConfiguration<QuizOption>
    {
        public void Configure(EntityTypeBuilder<QuizOption> builder)
        {
            builder.ToTable("QuizOptions");

            builder.HasKey(quizOption => quizOption.Id);

            builder.Property(quizOption => quizOption.Text)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(quizOption => quizOption.MediaUrl)
                .HasMaxLength(2048)
                .IsRequired(false);

            builder.Property(quizOption => quizOption.IsCorrect)
                .IsRequired();

            builder.HasOne(quizOption => quizOption.QuizQuestion)
                .WithMany(quizQuestion => quizQuestion.QuizOptions)
                .HasForeignKey(quizOption => quizOption.QuizQuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
