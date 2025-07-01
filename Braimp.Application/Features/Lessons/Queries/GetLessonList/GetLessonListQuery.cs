using MediatR;

namespace Braimp.Application.Features.Lessons.Queries.GetLessonList;
public class GetLessonListQuery : IRequest<LessonListResponse>
{
    public Guid ModuleId { get; set; }
    public Guid CourseId { get; set; }
}
