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
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasOne(courseParticipant => courseParticipant.Course)
            .WithMany(course => course.Participants)
            .HasForeignKey(courseParticipant => courseParticipant.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(courseParticipant => courseParticipant.User)
            .WithMany(user => user.Courses)
            .HasForeignKey(courseParticipant => courseParticipant.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
