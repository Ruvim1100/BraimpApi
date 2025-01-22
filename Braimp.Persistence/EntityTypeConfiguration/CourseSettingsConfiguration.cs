using Braimp.Domain.Entities;
using Braimp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class CourseSettingsConfiguration : IEntityTypeConfiguration<CourseSettings>
    {
        public void Configure(EntityTypeBuilder<CourseSettings> builder)
        {
            builder.ToTable("CourseSettings");

            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.GradingSystem)
                .HasDefaultValue(GradingSystem.PointsOutOf10)
                .IsRequired();

            builder.Property(cs => cs.CoverImageUrl)
                .HasMaxLength(2048)
                .IsRequired(false);

            builder.Property(cs => cs.BackgroundColor)
                .HasMaxLength(30)
                .IsRequired(false);

            builder.Property(cs => cs.LogoUrl)
                .HasMaxLength(2048)
                .IsRequired(false);

            builder.HasOne(cs => cs.Course)
                .WithOne(c => c.Settings)
                .HasForeignKey<CourseSettings>(cs => cs.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
