using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Notifications.Enums;

namespace Braimp.Domain.Entities.Notifications
{
    public class Notification : BaseEntity<Guid>, IAuditable
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public NotificationType Type { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
