using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Domain.Entities.Assignments;
public class Assignment : BaseEntity<Guid>, IAuditable
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? Deadline { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public ICollection<Submission> Submissions { get; set; } 
        = new List<Submission>();
    public ICollection<AssignmentFile> AssignmentFiles { get; set; }
    = new List<AssignmentFile>();
}
