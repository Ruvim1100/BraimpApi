using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Courses
{
    public class CourseCategory : BaseEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Course> Courses { get; set; } 
            = new List<Course>();
    }
}
