using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class CourseNewsConfiguration : IEntityTypeConfiguration<CourseNews>
    {
        public void Configure(EntityTypeBuilder<CourseNews> builder)
        {
            builder.ToTable("CourseNews");

            builder.HasKey(cn => cn.Id);

            builder.Property(cn => cn.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(cn => cn.Content)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(cn => cn.ImageUrl)
                .HasMaxLength(2048)
                .IsRequired(false);

            builder.Property(cn => cn.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(cn => cn.AuthorId)
                .IsRequired();

            builder.HasOne(cn => cn.Course)
                .WithMany(c => c.News)
                .HasForeignKey(cn => cn.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
