using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Domain.Entities.Quizzes;
public class Quiz : BaseEntity<Guid>, IAuditable
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? TimeLimitMinutes { get; set; }
    public bool IsPublished { get; set; }
    public int MaxAttempts { get; set; }
    public bool IsRandomized { get; set; }
    public int SortIndex { get; set; }
    public DateTimeOffset? AvailableFrom { get; set; }
    public DateTimeOffset? AvailableUntil { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public ICollection<QuizQuestion> Questions { get; set; }
        = new List<QuizQuestion>();

    public ICollection<QuizAttempt> QuizAttempts { get; set; }
        = new List<QuizAttempt>();
}
