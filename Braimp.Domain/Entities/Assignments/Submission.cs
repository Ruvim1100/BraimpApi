using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Assignments;
public class Submission : BaseEntity<Guid>, IAuditable
{
    public Guid StudentId { get; set; }
    public Guid? ReviewerId { get; set; }
    public string? Text { get; set; }
    public decimal? Grade { get; set; }
    public string? ReviewComment { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public DateTimeOffset? ReviewedAt { get; set; }
    public bool CanEdit { get; set; }
    public Guid AssignmentId { get; set; }
    public Guid FileResourceId { get; set; }
    public Assignment Assignment { get; set; } = null!;
}