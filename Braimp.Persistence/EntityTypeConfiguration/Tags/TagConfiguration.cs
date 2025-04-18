using Braimp.Domain.Entities.Tags;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(TableNames.Tags);

            builder.HasKey(tag => tag.Id);

            builder.Property(tag => tag.Name)
                .HasMaxLength(50);
        }
    }
}
