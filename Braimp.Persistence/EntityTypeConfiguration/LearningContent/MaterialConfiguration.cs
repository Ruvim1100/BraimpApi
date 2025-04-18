using Braimp.Domain.Entities.LearningContent;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration;
public class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable(TableNames.Materials);

        builder.HasKey(material => material.Id);

        builder.Property(material => material.Title)
            .HasMaxLength(100);

        builder.Property(material => material.Description)
            .HasMaxLength(500);

        builder.Property(material => material.ResourceUrl)
            .HasMaxLength(2048);

        builder.Property(material => material.ResourceType)
            .HasConversion<string>();

        builder.HasOne(material => material.Lesson)
            .WithMany(lesson => lesson.Materials)
            .HasForeignKey(material => material.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
