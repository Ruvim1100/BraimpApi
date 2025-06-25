using Braimp.Domain.Abstraction;

namespace Braimp.Domain.Entities.Quizzes;
public class QuizQuestionFile : BaseEntity<Guid>
{
    public Guid ResourceId { get; set; }

    public Guid QuizQuestionId { get; set; }
    public QuizQuestion QuizQuestion { get; set; } = null!;
}
