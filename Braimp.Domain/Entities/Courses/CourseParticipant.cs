using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;

namespace Braimp.Domain.Entities.Courses;
public class CourseParticipant : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public CourseRole Role { get; set; }
}
