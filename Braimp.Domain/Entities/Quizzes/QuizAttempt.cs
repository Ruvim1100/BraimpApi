using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Quizzes;
public class QuizAttempt : BaseEntity<Guid>
{
    public Guid StudentId { get; set; }
    public decimal Score { get; set; }
    public decimal? Grade { get; set; }
    public int CorrectAnswerCount { get; set; }
    public int IncorrectAnswerCount { get; set; }

    public DateTimeOffset StartedAt { get; set; }
    public DateTimeOffset? FinishedAt { get; set; }

    public int TimeLimitMinutes { get; set; }
    public bool IsPublished { get; set; }
    public int AttemptNumber { get; set; }

    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;

    public ICollection<AttemptAnswer> AttemptAnswers { get; set; } 
        = new List<AttemptAnswer>();
}
