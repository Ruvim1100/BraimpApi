using MediatR;

namespace Braimp.Application.Features.Lessons.Queries.GetPublishedLessonList;
public class GetPublishedLessonListQuery : IRequest<PublishedLessonListResponse>
{
    public Guid CourseId { get; set; }
    public Guid ModuleId { get; set; }
}
