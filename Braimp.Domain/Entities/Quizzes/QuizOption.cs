using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Quizzes;
public class QuizOption : BaseEntity<Guid>
{
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public Guid QuizQuestionId { get; set; }
    public QuizQuestion QuizQuestion { get; set; } = null!;
}