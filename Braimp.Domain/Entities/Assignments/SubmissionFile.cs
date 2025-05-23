using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Assignments;
public class SubmissionFile : BaseEntity<Guid>
{
    public Guid ResourceId { get; set; }
    public Guid SubmissionId { get; set; }
    public Submission Submission { get; set; } = null!;
}
