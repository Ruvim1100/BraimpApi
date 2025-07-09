using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Quizzes;
public class AnswerOption : BaseEntity<Guid>
{
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public bool IsSelected { get; set; }

    public Guid? OriginalOptionId { get; set; }

    public Guid AttemptAnswerId { get; set; }
    public AttemptAnswer AttemptAnswer { get; set; } = null!;
}
