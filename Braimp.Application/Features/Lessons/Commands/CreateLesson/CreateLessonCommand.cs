using MediatR;

namespace Braimp.Application.Features.Lessons.Commands.CreateLesson;
public class CreateLessonCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsPublished { get; set; } = false;
    public int SortIndex { get; set; }
    public Guid ModuleId { get; set; }
    public Guid CourseId { get; set; }
}
