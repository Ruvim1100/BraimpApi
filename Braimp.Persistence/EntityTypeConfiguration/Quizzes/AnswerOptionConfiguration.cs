using Braimp.Domain.Entities.Quizzes;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes;
public class AnswerOptionConfiguration : IEntityTypeConfiguration<AnswerOption>
{
    public void Configure(EntityTypeBuilder<AnswerOption> builder)
    {
        builder.ToTable(TableNames.AnswerOptions);

        builder.HasKey(option => option.Id);

        builder.Property(option => option.Text)
            .HasMaxLength(300);

        builder.HasOne(option => option.AttemptAnswer)
            .WithMany(answer => answer.AnswerOptions)
            .HasForeignKey(option => option.AttemptAnswerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
