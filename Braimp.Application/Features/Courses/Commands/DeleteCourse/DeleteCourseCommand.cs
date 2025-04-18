using MediatR;

namespace Braimp.Application.Features.Courses.Commands.DeleteCourse;
public class DeleteCourseCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
}
