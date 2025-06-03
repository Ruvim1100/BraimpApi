using Braimp.Domain.Entities.Quizzes;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes;
public class QuizQuestionFileConfiguration : IEntityTypeConfiguration<QuizQuestionFile>
{
    public void Configure(EntityTypeBuilder<QuizQuestionFile> builder)
    {
        builder.HasKey(quizQuestionFile => quizQuestionFile.Id);

        builder.ToTable(TableNames.QuizQuestionFiles);

        builder.HasOne(quizQuestionFile => quizQuestionFile.QuizQuestion)
            .WithOne(quizQuestion => quizQuestion.QuizQuestionFile)
            .HasForeignKey<QuizQuestionFile>(quizQuestionFile => quizQuestionFile.QuizQuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
