using AutoMapper;
using Braimp.Application.Features.Courses.Commands.CreateCourse;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Courses.Enums;

namespace Braimp.WebApi.Endpoints.Courses.CreateCourse;
public class Request : IMapWith<CreateCourseCommand>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public GradingSystem GradingSystem { get; set; }
    public Guid CourseCategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, CreateCourseCommand>();
    }
}
