using Braimp.Domain.Entities;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable(TableNames.Resources);

        builder.HasKey(resource => resource.Id);

        builder.Property(resource => resource.Name)
            .HasMaxLength(100);

        builder.Property(resource => resource.Url)
            .HasMaxLength(2048);
    }
}
