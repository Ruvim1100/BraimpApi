using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
public class CourseLookupDto : IMapWith<Course>
{

    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public string? Description { get; set; }
    public string CourseCategory { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Course, CourseLookupDto>()
            .ForMember(d => d.CourseCategory,
                       opt => opt.MapFrom(s => s.CourseCategory.Name))
            .ForMember(d => d.Status,
                       opt => opt.MapFrom(s => s.Status.ToString()));
    }
}
