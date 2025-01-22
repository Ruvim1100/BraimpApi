using Braimp.Domain.Common;
using Braimp.Domain.Enums;

namespace Braimp.Domain.Entities
{
    public class EnrollmentRequest : BaseEntity
    {
        public DateTime RequestedAt { get; set; }
        public EnrollmentStatus IsApproved { get; set; } = EnrollmentStatus.Pending;
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
