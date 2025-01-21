using Braimp.Domain.Common;

namespace Braimp.Domain.Entities
{
    public class Quiz : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? TimeLimitMinutes { get; set; }
        public bool IsVisibleToStudent { get; set; }
        public int MaxAttempts { get; set; }
        public bool IsRandomized { get; set; }
        public DateTime? StartTime { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<QuizQuestion> Questions { get; set; }
            = new List<QuizQuestion>();
        public ICollection<QuizResult> QuizResults { get; set; }
            = new List<QuizResult>();
    }
}
