using Braimp.Domain.Common;
using Braimp.Domain.Enums;

namespace Braimp.Domain.Entities
{
    public class CourseParticipant : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public CourseRole Role { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
