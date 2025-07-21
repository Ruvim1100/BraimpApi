using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;

namespace Braimp.Application.Features.Courses.Queries.GetPendingCoursesList;
public class PendingCourseLookupModel : IMapWith<Course>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CourseStatus Status { get; set; }
    public GradingSystem GradingSystem { get; set; } = GradingSystem.TenPoint;
    public Guid CourseCategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Course, PendingCourseLookupModel>();
    }
}
