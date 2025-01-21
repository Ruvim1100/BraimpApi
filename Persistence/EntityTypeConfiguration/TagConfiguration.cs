using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
