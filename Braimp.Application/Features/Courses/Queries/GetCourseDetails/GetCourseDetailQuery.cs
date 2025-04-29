using MediatR;

namespace Braimp.Application.Features.Courses.Queries.GetCourseDetails;
public class GetCourseDetailQuery : IRequest<CourseDetailsResponse>
{
    public Guid Id { get; set; }
}
