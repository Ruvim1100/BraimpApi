using Braimp.Domain.Entities.LearningContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.LearningContent
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons");

            builder.HasKey(lesson => lesson.Id);

            builder.Property(lesson => lesson.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(lesson => lesson.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(lesson => lesson.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(lesson => lesson.UpdatedAt)
                .IsRequired(false);

            builder.Property(lesson => lesson.IsVisibleToStudent)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(lesson => lesson.SortIndex)
                .IsRequired();

            builder.HasOne(lesson => lesson.Module)
                .WithMany(module => module.Lessons)
                .HasForeignKey(lesson => lesson.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
