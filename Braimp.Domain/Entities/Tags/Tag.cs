using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Tags
{
    public class Tag : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<CourseTag> Courses { get; set; } 
            = new List<CourseTag>();
    }
}
 