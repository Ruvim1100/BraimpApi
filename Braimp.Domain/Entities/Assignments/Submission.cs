using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Assignments.Enums;

namespace Braimp.Domain.Entities.Assignments
{
    public class Submission : BaseEntity<Guid>, IAuditable
    {
        public SubmissionStatus Status { get; set; } = SubmissionStatus.Pending;
        public Guid StudentId { get; set; }
        public Guid? ReviewerId { get; set; }
        public string? Text { get; set; }
        public decimal? Grade { get; set; }
        public string? ReviewComment { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? ReviewedAt { get; set; }
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!;

        public ICollection<SubmissionAttachment> SubmissionAttachments { get; set; }
            = new List<SubmissionAttachment>();
    }
}
