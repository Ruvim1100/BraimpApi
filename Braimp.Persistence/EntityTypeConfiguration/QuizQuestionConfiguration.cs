using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
    {
        public void Configure(EntityTypeBuilder<QuizQuestion> builder)
        {
            builder.ToTable("QuizQuestions");

            builder.HasKey(qq => qq.Id);

            builder.Property(qq => qq.Text)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(qq => qq.QuestionType)
                .IsRequired();

            builder.Property(qq => qq.MediaUrl)
                .HasMaxLength(2048)
                .IsRequired(false);

            builder.Property(qq => qq.Weight)
                .HasDefaultValue(1)
                .IsRequired();

            builder.HasOne(qq => qq.Quiz)
                .WithMany(q => q.Questions)
                .HasForeignKey(qq => qq.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
