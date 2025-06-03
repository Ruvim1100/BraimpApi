using MediatR;

namespace Braimp.Application.Features.Quizzes.Commands.DeleteQuiz;
public class DeleteQuizCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }   
}
