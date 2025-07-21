using Braimp.Domain.Entities.Courses.Enums;
using MediatR;

namespace Braimp.Application.Features.Courses.Commands.ReviewCourse;
public class ReviewCourseCommand : IRequest<Unit>
{
    public Guid CourseId { get; set; }
    public CourseStatus Status { get; set; }
}
