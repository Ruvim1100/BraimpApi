using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using Braimp.Domain.Entities.Users;

namespace Braimp.Domain.Entities.Courses;
public class EnrollmentRequest : BaseEntity<Guid>, IAuditable
{
    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Pending;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
}
