using Braimp.Domain.Entities.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Courses
{
    public class CourseParticipantConfiguration : IEntityTypeConfiguration<CourseParticipant>
    {
        public void Configure(EntityTypeBuilder<CourseParticipant> builder)
        {
            builder.ToTable("CourseParticipants");

            builder.HasKey(courseParticipant => courseParticipant.Id);

            builder.Property(courseParticipant => courseParticipant.UserId)
                .IsRequired();

            builder.Property(courseParticipant => courseParticipant.Role)
                .IsRequired();

            builder.HasOne(courseParticipant => courseParticipant.Course)
                .WithMany(course => course.Participants)
                .HasForeignKey(courseParticipant => courseParticipant.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
