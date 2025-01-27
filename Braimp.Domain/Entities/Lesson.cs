using Braimp.Domain.Common;

namespace Braimp.Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsVisibleToStudent { get; set; }
        public int SortIndex { get; set; }
        public Guid ModuleId { get; set; }
        public Module Module { get; set; }
        public ICollection<Material> Materials { get; set; }
            = new List<Material>();
    }
}
