using MediatR;

namespace Braimp.Application.Features.QuizAttempts.Commands.SubmitQuizAnswers;
public class SubmitQuizAnswersCommand : IRequest<Unit>
{
    public Guid QuizAttemptId { get; set; }
    public List<SubmitAnswerModel> Answers { get; set; } = new();
}
