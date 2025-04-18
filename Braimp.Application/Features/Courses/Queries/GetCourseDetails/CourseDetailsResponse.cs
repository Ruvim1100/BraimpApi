using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Application.Features.Courses.Queries.GetCourseDetails;
public class CourseDetailsResponse : IMapWith<Course>
{
    public Guid OwnerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public string CourseCategory { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Course, CourseDetailsResponse>()
            .ForMember(destination => destination.Status,
            opt => opt.MapFrom(source => source.Status.ToString()))

            .ForMember(destination => destination.CourseCategory,
            opt => opt.MapFrom(source => source.CourseCategory.Name))

            .ForMember(destination => destination.Tags,
            opt => opt.MapFrom(source => source.Tags.Select(ct => ct.Tag.Name).ToList()));
    }
}
