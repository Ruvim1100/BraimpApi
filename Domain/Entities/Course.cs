using Braimp.Domain.Common;
using Braimp.Domain.Enums;

namespace Braimp.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public CourseStatus Status { get; set; } = CourseStatus.Pending;
        public Guid OwnerId { get; set; }
        public DateTime CreatedAt { get; set; } 
        public CourseSettings Settings { get; set; }
        public Guid CourseCategoryId { get; set; }
        public CourseCategory CourseCategory { get; set; }
        public ICollection<EnrollmentRequest> EnrollmentRequests { get; set; }
            = new List<EnrollmentRequest>();
        public ICollection<CourseParticipant> Participants { get; set; }
            = new List<CourseParticipant>();
        public ICollection<CourseTag> Tags { get; set; }
            = new List<CourseTag>();
        public ICollection<Module> Modules { get; set; }
            = new List<Module>();
        public ICollection<CourseNews> News { get; set; }
            = new List<CourseNews>();
        public ICollection<Quiz> Quizzes { get; set; }
            = new List<Quiz>();
        public ICollection<Assignment> Assignments { get; set; }
            = new List<Assignment>();
        public ICollection<Notification> Notifications { get; set; }
            = new List<Notification>();
    }
}
