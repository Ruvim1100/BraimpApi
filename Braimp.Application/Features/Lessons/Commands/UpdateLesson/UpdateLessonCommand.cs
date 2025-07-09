using MediatR;

namespace Braimp.Application.Features.Lessons.Commands.UpdateLesson;
public class UpdateLessonCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool? IsPublished { get; set; }
    public Guid ModuleId { get; set; }
    public Guid CourseId { get; set; }
}
