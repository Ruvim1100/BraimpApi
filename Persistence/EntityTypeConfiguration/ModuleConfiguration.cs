using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Modules");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(m => m.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(m => m.IsVisibleToStudent)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(m => m.Order)
                .IsRequired();

            builder.HasOne(m => m.Course)
                .WithMany(c => c.Modules)
                .HasForeignKey(m => m.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
