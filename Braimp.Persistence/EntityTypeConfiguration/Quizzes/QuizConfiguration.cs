using Braimp.Domain.Entities.Quizzes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("Quizzes");

            builder.HasKey(quiz => quiz.Id);

            builder.Property(quiz => quiz.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(quiz => quiz.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(quiz => quiz.TimeLimitMinutes)
                .IsRequired(false);

            builder.Property(quiz => quiz.IsVisibleToStudent)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(quiz => quiz.MaxAttempts)
                .IsRequired();

            builder.Property(quiz => quiz.IsRandomized)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(quiz => quiz.StartTime)
                .IsRequired(false);

            builder.Property(quiz => quiz.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(quiz => quiz.UpdatedAt)
                .IsRequired(false);

            builder.HasOne(quiz => quiz.Course)
                .WithMany(course => course.Quizzes)
                .HasForeignKey(quiz => quiz.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
