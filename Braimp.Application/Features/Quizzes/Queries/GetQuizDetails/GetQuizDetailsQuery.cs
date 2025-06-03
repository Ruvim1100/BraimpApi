using MediatR;

namespace Braimp.Application.Features.Quizzes.Queries.GetQuizDetails;
public class GetQuizDetailsQuery : IRequest<QuizDetailsResponse>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
}
