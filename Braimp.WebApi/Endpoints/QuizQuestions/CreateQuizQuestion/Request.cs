using Braimp.Domain.Entities.Quizzes.Enums;

namespace Braimp.WebApi.Endpoints.QuizQuestions.CreateQuizQuestion;
public class Request
{
    public string Text { get; set; } = string.Empty;
    public QuestionType QuestionType { get; set; }
    public int Weight { get; set; } = 1;
    public Guid QuizId { get; set; }
    public Guid CourseId { get; set; }
    public IFormFile? File { get; set; }
    public string? DisplayName { get; set; }
    public IEnumerable<OptionModel>? QuizOptions { get; set; }
}
