using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Domain.Entities.Courses;

namespace Braimp.Application.Features.Courses.Queries.GetCourseDetails
{
    public class CourseDetailsVm : IMapWith<Course>
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
            profile.CreateMap<Course, CourseDetailsVm>()
                .ForMember(courseVm => courseVm.Status,
                opt => opt.MapFrom(course => course.Status.ToString()))

                .ForMember(courseVm => courseVm.CourseCategory,
                opt => opt.MapFrom(course => course.CourseCategory.Name))

                .ForMember(courseVm => courseVm.Tags,
                opt => opt.MapFrom(course => course.Tags.Select(ct => ct.Tag.Name).ToList()));
        }
    }
}
