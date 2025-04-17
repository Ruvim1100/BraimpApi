using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Application.Features.Courses.Commands.CreateCourse;

namespace Braimp.WebApi.Models
{
    public class CreateCourseDto : IMapWith<CreateCourseCommand>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Guid CourseCategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCourseDto, CreateCourseCommand>();
        }
    }
}
