using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Courses;
public class CourseImage : BaseEntity<Guid>
{
    public Guid ResourceId { get; set; }

    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
}
