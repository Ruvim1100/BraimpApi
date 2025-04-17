using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Courses
{
    public class CourseNews : BaseEntity<Guid>, IAuditable
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid AuthorId { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
