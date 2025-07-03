using Braimp.Domain.Entities.LearningContent;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.LearningContent;
public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.ToTable(TableNames.Modules);

        builder.HasKey(module => module.Id);

        builder.Property(module => module.Title)
            .HasMaxLength(100);

        builder.Property(module => module.IsPublished)
            .HasDefaultValue(true);

        builder.HasOne(module => module.Course)
            .WithMany(course => course.Modules)
            .HasForeignKey(module => module.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
