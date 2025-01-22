using Braimp.Domain.Common;

namespace Braimp.Domain.Entities
{
    public class CourseCategory : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; } 
            = new List<Course>();
    }
}
