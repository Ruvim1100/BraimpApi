using MediatR;

namespace Braimp.Application.Features.Quizzes.Queries.GetQuizzes;
public class GetQuizListQuery : IRequest<QuizListResponse>
{
    public Guid CourseId { get; set; }
}
