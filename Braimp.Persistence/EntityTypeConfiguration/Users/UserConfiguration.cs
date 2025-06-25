using Braimp.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Users;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Name)
            .HasMaxLength(20);

        builder.Property(user => user.Surname)
            .HasMaxLength(30);

        builder.Property(user => user.GivenName)
            .HasMaxLength(30);

        builder.Property(user => user.Country)
            .HasMaxLength(100);
    }
}
