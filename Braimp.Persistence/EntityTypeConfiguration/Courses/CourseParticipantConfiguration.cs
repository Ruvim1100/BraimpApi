using Braimp.Domain.Entities.Courses;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses;
public class CourseParticipantConfiguration : IEntityTypeConfiguration<CourseParticipant>
{
    public void Configure(EntityTypeBuilder<CourseParticipant> builder)
    {
        builder.ToTable(TableNames.CourseParticipants);

        builder.HasKey(courseParticipant => courseParticipant.Id);
        builder.Property(courseParticipant => courseParticipant.Role)
            .HasConversion<string>();

        builder.HasOne(courseParticipant => courseParticipant.Course)
            .WithMany(course => course.Participants)
            .HasForeignKey(courseParticipant => courseParticipant.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
