using Braimp.Domain.Entities.Quizzes;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes;
public class AttemptAnswerConfiguration : IEntityTypeConfiguration<AttemptAnswer>
{
    public void Configure(EntityTypeBuilder<AttemptAnswer> builder)
    {
        builder.ToTable(TableNames.AttemptAnswers); 

        builder.HasKey(answer => answer.Id);

        builder.Property(answer => answer.QuestionText)
            .HasMaxLength(300);

        builder.Property(answer => answer.Weight)
            .HasDefaultValue(1);

        builder.Property(answer => answer.QuestionType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(answer => answer.QuizAttempt)
            .WithMany(attempt => attempt.AttemptAnswers)
            .HasForeignKey(answer => answer.QuizAttemptId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
