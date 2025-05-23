using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Assignments;

public class AssignmentFile : BaseEntity<Guid>
{
    public Guid ResourceId { get; set; }
    public Guid AssignmentId { get; set; }
    public Assignment Assignment { get; set; } = null!;
}
