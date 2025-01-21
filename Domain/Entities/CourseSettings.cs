using Braimp.Domain.Common;
using Braimp.Domain.Enums;

namespace Braimp.Domain.Entities
{
    public class CourseSettings : BaseEntity
    {
        public GradingSystem GradingSystem { get; set; } 
            = GradingSystem.Points10;
        public string? CoverImageUrl { get; set; }
        public string? BackgroundColor { get; set; }
        public string? LogoUrl { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }

    }
}
