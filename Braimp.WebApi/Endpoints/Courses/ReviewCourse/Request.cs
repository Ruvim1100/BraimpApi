using Braimp.Domain.Entities.Courses.Enums;

namespace Braimp.WebApi.Endpoints.Courses.ReviewCourse;
public class Request
{
    public Guid Id { get; set; }
    public CourseStatus Status { get; set; }
}
