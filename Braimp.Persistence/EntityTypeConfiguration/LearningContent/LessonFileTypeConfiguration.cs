using Braimp.Domain.Entities.LearningContent;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.LearningContent;
public class LessonFileTypeConfiguration : IEntityTypeConfiguration<LessonFile>
{
    public void Configure(EntityTypeBuilder<LessonFile> builder)
    {
        builder.ToTable(TableNames.LessonFiles);

        builder.HasKey(lessonFile => lessonFile.Id);

        builder.HasOne(lessonFile => lessonFile.Lesson)
            .WithMany(lesson => lesson.LessonFiles)
            .HasForeignKey(lessonFile => lessonFile.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
