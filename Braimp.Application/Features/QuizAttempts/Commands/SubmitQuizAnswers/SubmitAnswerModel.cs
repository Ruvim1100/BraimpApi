using Braimp.Domain.Entities.Quizzes.Enums;

namespace Braimp.Application.Features.QuizAttempts.Commands.SubmitQuizAnswers;
public class SubmitAnswerModel
{
    public Guid QuestionId { get; set; }
    public QuestionType Type { get; set; }
    public string? TextAnswer { get; set; }
    public List<Guid>? SelectedOptionIds { get; set; }
    public Guid? SelectedOptionId { get; set; }
}
