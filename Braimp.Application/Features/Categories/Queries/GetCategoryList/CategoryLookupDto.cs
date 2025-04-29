using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Application.Features.Categories.Queries.GetCategoryList;
public class CategoryLookupDto : IMapWith<CourseCategory>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CourseCategory, CategoryLookupDto>();
    }
}
