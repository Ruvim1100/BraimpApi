using Braimp.Domain.Common;

namespace Braimp.Domain.Entities
{
    public class CourseNews : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid AuthorId { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
