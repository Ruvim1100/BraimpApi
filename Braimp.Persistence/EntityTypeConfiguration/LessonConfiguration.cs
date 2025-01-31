using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("Lessons");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(l => l.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(l => l.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(l => l.IsVisibleToStudent)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(l => l.SortIndex)
                .IsRequired();

            builder.HasOne(l => l.Module)
                .WithMany(m => m.Lessons)
                .HasForeignKey(l => l.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
