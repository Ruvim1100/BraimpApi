using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Quizzes;
public class QuizResult : BaseEntity<Guid>, IAuditable
{
    public Guid StudentId { get; set; }
    public decimal Score { get; set; }
    public decimal? Grade { get; set; }
    public int CorrectAnswerCount { get; set; } //dynamic or update
    public int IncorrectAnswerCount { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsPublished { get; set; }
    public int AttemptNumber { get; set; }
    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;
}
