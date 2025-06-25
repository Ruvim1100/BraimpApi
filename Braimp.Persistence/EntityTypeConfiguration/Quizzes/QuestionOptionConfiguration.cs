using Braimp.Domain.Entities.Quizzes;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes;
public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder.ToTable(TableNames.QestionOptions);

        builder.HasKey(quizOption => quizOption.Id);

        builder.Property(quizOption => quizOption.Text)
            .HasMaxLength(100);

        builder.HasOne(quizOption => quizOption.QuizQuestion)
            .WithMany(quizQuestion => quizQuestion.QuestionOptions)
            .HasForeignKey(quizOption => quizOption.QuizQuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
