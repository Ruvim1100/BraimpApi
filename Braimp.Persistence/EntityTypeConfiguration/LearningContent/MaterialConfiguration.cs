using Braimp.Domain.Entities.LearningContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("Materials");

            builder.HasKey(material => material.Id);

            builder.Property(material => material.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(material => material.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(material => material.ResourceType)
                .IsRequired();

            builder.Property(material => material.ResourceUrl)
                .HasMaxLength(2048)
                .IsRequired();

            builder.Property(material => material.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(material => material.UpdatedAt)
                .IsRequired(false);

            builder.HasOne(material => material.Lesson)
                .WithMany(lesson => lesson.Materials)
                .HasForeignKey(material => material.LessonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
