using MediatR;

namespace Braimp.Application.Features.Lessons.Commands.DeleteLesson;
public class DeleteLessonCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid ModuleId { get; set; }
    public Guid CourseId { get; set; }
}
