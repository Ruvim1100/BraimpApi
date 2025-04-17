using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.LearningContent.Enums;

namespace Braimp.Domain.Entities.LearningContent
{
    public class Material : BaseEntity<Guid>, IAuditable
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ResourceType ResourceType { get; set; }  
        public string ResourceUrl { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;
    }
}
