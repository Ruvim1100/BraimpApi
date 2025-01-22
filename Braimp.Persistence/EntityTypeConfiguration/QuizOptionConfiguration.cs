using Braimp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Persistence.EntityTypeConfiguration
{
    public class QuizOptionConfiguration : IEntityTypeConfiguration<QuizOption>
    {
        public void Configure(EntityTypeBuilder<QuizOption> builder)
        {
            builder.ToTable("QuizOptions");

            builder.HasKey(qo => qo.Id);

            builder.Property(qo => qo.Text)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(qo => qo.MediaUrl)
                .HasMaxLength(2048)
                .IsRequired(false);

            builder.Property(qo => qo.IsCorrect)
                .IsRequired();

            builder.HasOne(qo => qo.QuizQuestion)
                .WithMany(qq => qq.QuizOptions)
                .HasForeignKey(qo => qo.QuizQuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
