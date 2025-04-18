using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Domain.Entities.Tags;
public class CourseTag : BaseEntity<Guid>
{
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public Guid TagId { get; set; }
    public Tag Tag { get; set; } = null!;
}
