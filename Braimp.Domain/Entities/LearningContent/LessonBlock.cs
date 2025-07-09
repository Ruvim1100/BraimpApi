using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.LearningContent.Enums;

namespace Braimp.Domain.Entities.LearningContent;
public class LessonBlock : BaseEntity<Guid>
{
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
    public LessonBlockType BlockType { get; set; }
    public string Content { get; set; } = string.Empty;
    public int SortIndex { get; set; }
}
