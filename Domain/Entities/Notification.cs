using Braimp.Domain.Common;
using Braimp.Domain.Enums;

namespace Braimp.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public NotificationType Type { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
