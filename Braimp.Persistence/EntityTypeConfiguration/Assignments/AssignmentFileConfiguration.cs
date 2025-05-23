using Braimp.Domain.Entities.Assignments;
using Braimp.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Braimp.Infrastructure.EntityTypeConfiguration.Assignments;
internal class AssignmentFileConfiguration : IEntityTypeConfiguration<AssignmentFile>
{
    public void Configure(EntityTypeBuilder<AssignmentFile> builder)
    {
        builder.ToTable(TableNames.AssignmentFiles);

        builder.HasKey(assignmentFile => assignmentFile.Id);

        builder.HasOne(assignmentFile => assignmentFile.Assignment)
            .WithMany(assignment => assignment.AssignmentFiles)
            .HasForeignKey(assignmentFile => assignmentFile.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
