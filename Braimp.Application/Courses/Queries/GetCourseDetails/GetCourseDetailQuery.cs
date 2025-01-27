using MediatR;

namespace Braimp.Application.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailQuery : IRequest<CourseDetailsVm>
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
    }
}
