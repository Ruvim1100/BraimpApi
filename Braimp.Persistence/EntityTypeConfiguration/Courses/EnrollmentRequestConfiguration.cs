using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses
{
    public class EnrollmentRequestConfiguration : IEntityTypeConfiguration<EnrollmentRequest>
    {
        public void Configure(EntityTypeBuilder<EnrollmentRequest> builder)
        {
            builder.ToTable("EnrollmentRequests");

            builder.HasKey(enrollment => enrollment.Id);

            builder.Property(enrollment =>enrollment.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(enrollment => enrollment.UpdatedAt)
                .IsRequired(false);

            builder.Property(enrollment => enrollment.Status)
                .HasDefaultValue(EnrollmentStatus.Pending)
                .IsRequired();

            builder.Property(enrollment => enrollment.UserId)
                .IsRequired();

            builder.HasOne(enrollment => enrollment.Course)
                .WithMany(course => course.EnrollmentRequests)
                .HasForeignKey(enrollment =>enrollment.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
