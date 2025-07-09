using MediatR;

namespace Braimp.Application.Features.QuizAttempts.Commands.CreateQuizAttempt;
public class CreateQuizAttemptCommand : IRequest<QuizAttemptCreatedModel>
{
    public Guid CourseId { get; set; }
    public Guid QuizId { get; set; }
}
