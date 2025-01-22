using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class CourseParticipantConfiguration : IEntityTypeConfiguration<CourseParticipant>
    {
        public void Configure(EntityTypeBuilder<CourseParticipant> builder)
        {
            builder.ToTable("CourseParticipants");

            builder.HasKey(cp => cp.Id);

            builder.Property(cp => cp.UserId)
                .IsRequired();

            builder.Property(cp => cp.Role)
                .IsRequired();

            builder.Property(cp => cp.JoinedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.HasOne(cp => cp.Course)
                .WithMany(c => c.Participants)
                .HasForeignKey(cp => cp.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
