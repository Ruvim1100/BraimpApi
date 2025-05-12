using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Application.Pagination;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
public class CourseListResponse : PaginationResult<CourseListResponse.Item>
{
    public CourseListResponse(List<Item> items, int page, int pageSize, int totalCount) : base(items, page, pageSize, totalCount)
    {
    }

    public class Item : IMapWith<Course>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public string? Description { get; set; }
        public string CourseCategory { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, Item>()
                .ForMember(d => d.CourseCategory,
                           opt => opt.MapFrom(s => s.CourseCategory.Name))
                .ForMember(d => d.Status,
                           opt => opt.MapFrom(s => s.Status.ToString()));
        }
    }
}
