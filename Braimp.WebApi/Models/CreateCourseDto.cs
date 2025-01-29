using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Application.Courses.Commands.CreateCourse;

namespace Braimp.WebApi.Models
{
    public class CreateCourseDto : IMapWith<CreateCourseCommand>
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Guid CourseCategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCourseDto, CreateCourseCommand>();
        }
    }
}
