using Braimp.Domain.Common;

namespace Braimp.Domain.Entities
{
    public class Assignment : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? AttachmentUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Submission> Submissions { get; set; } 
            = new List<Submission>();
    }
}
