using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.LearningContent;
public class LessonFile : BaseEntity<Guid>
{
    public Guid ResourceId { get; set; }
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
}
