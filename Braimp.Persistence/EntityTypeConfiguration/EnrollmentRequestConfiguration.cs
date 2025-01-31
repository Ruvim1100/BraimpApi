using Braimp.Domain.Entities;
using Braimp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class EnrollmentRequestConfiguration : IEntityTypeConfiguration<EnrollmentRequest>
    {
        public void Configure(EntityTypeBuilder<EnrollmentRequest> builder)
        {
            builder.ToTable("EnrollmentRequest");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.RequestedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(e => e.Status)
                .HasDefaultValue(EnrollmentStatus.Pending)
                .IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.HasOne(e => e.Course)
                .WithMany(c => c.EnrollmentRequests)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
