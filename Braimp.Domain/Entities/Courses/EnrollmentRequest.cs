using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;

namespace Braimp.Domain.Entities.Courses
{
    public class EnrollmentRequest : BaseEntity<Guid>, IAuditable
    {
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Pending;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
