using Braimp.Domain.Entities.LearningContent;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.LearningContent;
public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable(TableNames.Lessons);

        builder.HasKey(lesson => lesson.Id);

        builder.Property(lesson => lesson.Title)
            .HasMaxLength(100);

        builder.Property(lesson => lesson.Description)
            .HasMaxLength(1000);

        builder.Property(lesson => lesson.IsPublished)
            .HasDefaultValue(true);

        builder.HasOne(lesson => lesson.Module)
            .WithMany(module => module.Lessons)
            .HasForeignKey(lesson => lesson.ModuleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
