using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Quizzes.Enums;

namespace Braimp.Domain.Entities.Quizzes;
public class AttemptAnswer : BaseEntity<Guid>
{
    public string QuestionText { get; set; } = string.Empty;
    public QuestionType QuestionType { get; set; }
    public int Weight { get; set; }

    public Guid QuizAttemptId { get; set; }
    public QuizAttempt QuizAttempt { get; set; } = null!;

    public ICollection<AnswerOption> AnswerOptions { get; set; } 
        = new List<AnswerOption>();

    public ICollection<AnswerText> AnswerTexts { get; set; }
        = new List<AnswerText>();
}
