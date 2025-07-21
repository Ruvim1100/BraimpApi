using MediatR;

namespace Braimp.Application.Features.Courses.Commands.CreateCourse;
public class CreateCourseCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid CourseCategoryId { get; set; }
}
