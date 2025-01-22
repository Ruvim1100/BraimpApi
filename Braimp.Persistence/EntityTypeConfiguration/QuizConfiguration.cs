using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("Quizzes");

            builder.HasKey(q => q.Id);

            builder.Property(q => q.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(q => q.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(q => q.TimeLimitMinutes)
                .IsRequired(false);

            builder.Property(q => q.IsVisibleToStudent)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(q => q.MaxAttempts)
                .IsRequired();

            builder.Property(q => q.IsRandomized)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(q => q.StartTime)
                .IsRequired(false);

            builder.HasOne(q => q.Course)
                .WithMany(c => c.Quizzes)
                .HasForeignKey(q => q.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
