using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Assignments
{
    public class SubmissionAttachment : BaseEntity<Guid>
    {
        public string FileUrl { get; set; } = string.Empty;
        public Guid SubmissionId { get; set; }
        public Submission Submission { get; set; } = null!;
    }
}
