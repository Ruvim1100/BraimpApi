using Braimp.Domain.Abstraction;
using Braimp.Domain.Entities.Quizzes.Enums;

namespace Braimp.Domain.Entities.Quizzes;
public class QuizQuestion : BaseEntity<Guid>
{
    public string Text { get; set; } = string.Empty;
    public QuestionType QuestionType { get; set; }
    public int Weight { get; set; } = 1;

    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;

    public QuizQuestionFile? QuizQuestionFile { get; set; }

    public ICollection<QuestionOption> QuestionOptions { get; set; } 
        = new List<QuestionOption>();

}
