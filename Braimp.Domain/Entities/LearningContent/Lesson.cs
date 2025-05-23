using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.LearningContent;
public class Lesson : BaseEntity<Guid>, IAuditable
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsPublished { get; set; }
    public int SortIndex { get; set; }
    public Guid ModuleId { get; set; }
    public Module Module { get; set; } = null!;
    public ICollection<LessonFile> LessonFiles { get; set; }
        = new List<LessonFile>();
}
