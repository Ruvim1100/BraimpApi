using Braimp.Domain.Entities.LearningContent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.LearningContent
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Modules");

            builder.HasKey(module => module.Id);

            builder.Property(module => module.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(module => module.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(module => module.IsVisibleToStudent)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(module => module.SortIndex)
                .IsRequired();

            builder.Property(module => module.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(module => module.UpdatedAt)
                .IsRequired(false);

            builder.HasOne(module => module.Course)
                .WithMany(course => course.Modules)
                .HasForeignKey(module => module.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
