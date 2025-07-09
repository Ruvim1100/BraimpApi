using MediatR;

namespace Braimp.Application.Features.QuizQuestions.Commands.DeleteQuizQuestion;
public class DeleteQuizQuestionCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public Guid CourseId { get; set; }
}

