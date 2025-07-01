using Braimp.Domain.Entities.Quizzes;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes;
public class AnswerTextConfiguration : IEntityTypeConfiguration<AnswerText>
{
    public void Configure(EntityTypeBuilder<AnswerText> builder)
    {
        builder.ToTable(TableNames.AnswerTexts);

        builder.HasKey(text => text.Id);

        builder.Property(text => text.Text)
            .HasMaxLength(300);

        builder.HasOne(text => text.AttemptAnswer)
            .WithMany(answer => answer.AnswerTexts)
            .HasForeignKey(text => text.AttemptAnswerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
