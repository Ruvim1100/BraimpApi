using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Quizzes;
public class AnswerText : BaseEntity<Guid>
{
    public string Text { get; set; } = string.Empty;
    public Guid AttemptAnswerId { get; set; }
    public AttemptAnswer AttemptAnswer { get; set; } = null!;
}
