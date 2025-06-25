using Braimp.Domain.Entities.Courses.Enums;
using MediatR;

namespace Braimp.Application.Features.Courses.Commands.UpdateCourse;
public class UpdateCourseCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public GradingSystem? GradingSystem { get; set; }
    public Guid? CourseCategoryId { get; set; }
}
