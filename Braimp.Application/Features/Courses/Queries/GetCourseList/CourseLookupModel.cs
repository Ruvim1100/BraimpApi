using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
public class CourseLookupModel : IMapWith<Course>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? ThumbnailImage { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Course, CourseLookupModel>();
    }
}
