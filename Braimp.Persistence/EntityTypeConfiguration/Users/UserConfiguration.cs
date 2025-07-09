using Braimp.Domain.Entities.Users;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Users;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(user => user.Name)
            .HasMaxLength(50);

        builder.Property(user => user.Surname)
            .HasMaxLength(50);

        builder.Property(user => user.GivenName)
            .HasMaxLength(50);

        builder.Property(user => user.Country)
            .HasMaxLength(100);

        builder.Property(user => user.ProfileImageUrl)
            .HasMaxLength(500);
    }
}
