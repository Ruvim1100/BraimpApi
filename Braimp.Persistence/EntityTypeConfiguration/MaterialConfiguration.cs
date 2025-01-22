using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materials");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(m => m.ResourceType)
                .IsRequired();

            builder.Property(m => m.ResourceUrl)
                .HasMaxLength(2048)
                .IsRequired();

            builder.Property(m => m.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.HasOne(m => m.Lesson)
                .WithMany(l => l.Materials)
                .HasForeignKey(m => m .LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
