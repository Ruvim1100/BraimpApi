using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Domain.Entities.LearningContent
{
    public class Module : BaseEntity<Guid>, IAuditable
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsVisibleToStudent { get; set; }
        public int SortIndex { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public ICollection<Lesson> Lessons { get; set; }
            = new List<Lesson>();

    }
}
