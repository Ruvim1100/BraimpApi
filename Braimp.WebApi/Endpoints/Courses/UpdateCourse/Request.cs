using AutoMapper;
using Braimp.Application.Features.Courses.Commands.UpdateCourse;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Courses.Enums;

namespace Braimp.WebApi.Endpoints.Courses.UpdateCourse;
public class Request : IMapWith<UpdateCourseCommand>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public GradingSystem GradingSystem { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? BackgroundColor { get; set; }
    public string? LogoUrl { get; set; }
    public Guid? CourseCategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, UpdateCourseCommand>();
    }
}
