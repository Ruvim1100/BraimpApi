using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Domain.Entities.Quizzes;
public class Quiz : BaseEntity<Guid>, IAuditable
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? TimeLimitMinutes { get; set; }
    public bool IsVisibleToStudent { get; set; }
    public int MaxAttempts { get; set; }
    public bool IsRandomized { get; set; }
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public ICollection<QuizQuestion> Questions { get; set; }
        = new List<QuizQuestion>();
    public ICollection<QuizResult> QuizResults { get; set; }
        = new List<QuizResult>();
}
