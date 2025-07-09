using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using Braimp.Domain.Entities.Users;

namespace Braimp.Domain.Entities.Courses;
public class CourseParticipant : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public CourseRole Role { get; set; }
}
