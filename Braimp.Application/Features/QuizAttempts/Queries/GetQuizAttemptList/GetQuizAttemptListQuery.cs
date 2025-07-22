using MediatR;

namespace Braimp.Application.Features.QuizAttempts.Queries.GetQuizAttemptList;
public class GetQuizAttemptListQuery : IRequest<QuizAttemptListResponse>
{
    public Guid CourseId { get; set; }
    public Guid QuizId { get; set; }
}
