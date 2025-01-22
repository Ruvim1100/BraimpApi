using Braimp.Domain.Common;

namespace Braimp.Domain.Entities
{
    public class CourseTag : BaseEntity
    {
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
