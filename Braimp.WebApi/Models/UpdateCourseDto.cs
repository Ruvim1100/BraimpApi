using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Application.Courses.Commands.UpdateCourse;

namespace Braimp.WebApi.Models
{
    public class UpdateCourseDto : IMapWith<UpdateCourseCommand>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid? CourseCategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCourseDto, UpdateCourseCommand>();
        }
    }
}
