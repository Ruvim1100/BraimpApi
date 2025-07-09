using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Braimp.Application.Features.Courses.Commands.UpdateCourse;
public class UpdateCourseCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CourseStatus? Status { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public GradingSystem? GradingSystem { get; set; }
    public Guid? CourseCategoryId { get; set; }
    public List<Guid> TagIds { get; set; } = new();
}
