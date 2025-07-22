using Braimp.Application.Features.QuizAttempts.Commands.SubmitQuizAnswers;

namespace Braimp.WebApi.Endpoints.QuizAttempts.SubmitQuizAttempt;
public class Request
{
    public List<SubmitAnswerModel> Answers { get; set; } = new();
}
