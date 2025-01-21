using Braimp.Domain.Common;

namespace Braimp.Domain.Entities
{
    public class SubmissionAttachment : BaseEntity
    {
        public string FileUrl { get; set; }
        public Guid SubmissionId { get; set; }
        public Submission Submission { get; set; }
    }
}
