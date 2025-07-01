using MediatR;

namespace Braimp.Application.Features.Quizzes.Queries.GetPublishedQuizzes;
public class GetPublishedQuizListQuery : IRequest<PublishedQuizListResponse>
{
    public Guid CourseId { get; set; }
}
