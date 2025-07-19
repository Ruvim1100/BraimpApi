using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Application.Features.News.Queries.GetCourseNewsList;
public class CourseNewsLookupModel : IMapWith<CourseNews>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid AuthorId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CourseNews, CourseNewsLookupModel>();
    }
}
