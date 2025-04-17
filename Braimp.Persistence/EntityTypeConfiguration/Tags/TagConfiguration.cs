using Braimp.Domain.Entities.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");

            builder.HasKey(tag => tag.Id);

            builder.Property(tag => tag.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
