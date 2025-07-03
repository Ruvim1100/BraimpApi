using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;

namespace Braimp.Application.Features.Courses.Queries.GetCourseDetails;
public class CourseDetailsResponse : IMapWith<Course>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = string.Empty;
    public GradingSystem GradingSystem { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? ThumbnailImageUrl { get; set; }
    public string? BannerImageUrl { get; set; }
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
            opt => opt.MapFrom(source => source.Tags.Select(ct => ct.Tag.Name).ToList()))

            .ForMember(destination => destination.ThumbnailImageUrl, opt => opt.Ignore())
            .ForMember(destination => destination.BannerImageUrl, opt => opt.Ignore());

    }
}
