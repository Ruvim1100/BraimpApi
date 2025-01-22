using Braimp.Domain.Common;
using Braimp.Domain.Enums;

namespace Braimp.Domain.Entities
{
    public class Material : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public ResourceType ResourceType { get; set; }  
        public string ResourceUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
