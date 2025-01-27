using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Domain.Entities;

namespace Braimp.Application.Courses.Queries.GetCourseList
{
    public class CourseLookupDto : IMapWith<Course>
    {
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; } 
        public string CourseCategory { get; set; }
        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Course, CourseLookupDto>()
                .ForMember(courseDto => courseDto.CourseCategory, 
                opt => opt.MapFrom(course => course.CourseCategory.Name))
                
                .ForMember(courseDto => courseDto.Status,
                opt => opt.MapFrom(course => course.Status.ToString()));
        }
    }
}
