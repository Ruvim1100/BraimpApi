using Braimp.Domain.Entities.Quizzes;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Quizzes
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable(TableNames.Quizzes);

            builder.HasKey(quiz => quiz.Id);

            builder.Property(quiz => quiz.Title)
                .HasMaxLength(100);

            builder.Property(quiz => quiz.Description)
                .HasMaxLength(500);

            builder.Property(quiz => quiz.IsVisibleToStudent)
                .HasDefaultValue(true);

            builder.Property(quiz => quiz.IsRandomized)
                .HasDefaultValue(false);

            builder.HasOne(quiz => quiz.Course)
                .WithMany(course => course.Quizzes)
                .HasForeignKey(quiz => quiz.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
