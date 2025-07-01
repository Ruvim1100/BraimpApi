using MediatR;

namespace Braimp.Application.Features.QuizQuestions.Queries.GetQuizQuestionList;
public class GetQuizQuestionListQuery : IRequest<QuizQestionListResponse>
{
    public Guid CourseId { get; set; }
    public Guid QuizId { get; set; }
}
