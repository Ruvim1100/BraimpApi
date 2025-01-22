using Braimp.Domain.Common;
using Braimp.Domain.Enums;

namespace Braimp.Domain.Entities
{
    public class Submission : BaseEntity
    {
        public SubmissionStatus Status { get; set; } = SubmissionStatus.Pending;
        public Guid StudentId { get; set; }
        public Guid? ReviewerId { get; set; }
        public string? Text { get; set; }
        public decimal? Grade { get; set; }
        public string? ReviewComment { get; set; }
        public DateTime SubmittedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }

        public ICollection<SubmissionAttachment> SubmissionAttachments { get; set; }
            = new List<SubmissionAttachment>();
    }
}
