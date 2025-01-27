using MediatR;

namespace Braimp.Application.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
    }
}
