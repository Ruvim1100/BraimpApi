using Braimp.Domain.Entities.LearningContent;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.LearningContent;
public class LessonBlockConfiguration : IEntityTypeConfiguration<LessonBlock>
{
    public void Configure(EntityTypeBuilder<LessonBlock> builder)
    {
        builder.ToTable(TableNames.LessonBlocks);

        builder.HasKey(block => block.Id);

        builder.Property(block => block.BlockType)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.Property(block => block.Content)
            .HasMaxLength(10000);

        builder.HasOne(block => block.Lesson)
            .WithMany(lesson => lesson.LessonBlocks)
            .HasForeignKey(block => block.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
