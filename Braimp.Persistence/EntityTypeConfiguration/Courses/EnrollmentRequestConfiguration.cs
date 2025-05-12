using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses;
public class EnrollmentRequestConfiguration : IEntityTypeConfiguration<EnrollmentRequest>
{
    public void Configure(EntityTypeBuilder<EnrollmentRequest> builder)
    {
        builder.ToTable(TableNames.EnrollmentRequests);

        builder.HasKey(enrollment => enrollment.Id);

        builder.Property(enrollment => enrollment.Status)
            .HasConversion<string>()
            .HasDefaultValue(EnrollmentStatus.Pending);

        builder.HasOne(enrollment => enrollment.Course)
            .WithMany(course => course.EnrollmentRequests)
            .HasForeignKey(enrollment =>enrollment.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
