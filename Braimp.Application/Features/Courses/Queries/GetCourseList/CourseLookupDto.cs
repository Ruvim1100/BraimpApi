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
            .ForMember(courseDto => courseDto.CourseCategory, 
            opt => opt.MapFrom(course => course.CourseCategory.Name))
            
            .ForMember(courseDto => courseDto.Status,
            opt => opt.MapFrom(course => course.Status.ToString()));
    }
}
