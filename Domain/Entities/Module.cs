using Braimp.Domain.Common;

namespace Braimp.Domain.Entities
{
    public class Module : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsVisibleToStudent { get; set; }
        public int Order { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
            = new List<Lesson>();

    }
}
