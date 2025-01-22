using Braimp.Domain.Common;

namespace Braimp.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<CourseTag> Courses { get; set; } 
            = new List<CourseTag>();
    }
}
 