using Braimp.Domain.Entities.Quizzes;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes;
public class QuizOptionConfiguration : IEntityTypeConfiguration<QuizOption>
{
    public void Configure(EntityTypeBuilder<QuizOption> builder)
    {
        builder.ToTable(TableNames.QuizOptions);

        builder.HasKey(quizOption => quizOption.Id);

        builder.Property(quizOption => quizOption.Text)
            .HasMaxLength(100);

        builder.Property(quizOption => quizOption.MediaUrl)
            .HasMaxLength(2048);

        builder.HasOne(quizOption => quizOption.QuizQuestion)
            .WithMany(quizQuestion => quizQuestion.QuizOptions)
            .HasForeignKey(quizOption => quizOption.QuizQuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
