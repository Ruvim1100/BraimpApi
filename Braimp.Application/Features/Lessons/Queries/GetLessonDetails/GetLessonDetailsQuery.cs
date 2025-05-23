using MediatR;

namespace Braimp.Application.Features.Lessons.Queries.GetLessonDetails;
public class GetLessonDetailsQuery : IRequest<LessonDetailsResponse>
{
    public Guid Id { get; set; }
    public Guid ModuleId { get; set; }
    public Guid CourseId { get; set; }
}
