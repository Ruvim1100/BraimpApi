using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Application.Features.Courses.Commands.UpdateCourse;
using Braimp.Domain.Entities.Courses.Enums;

namespace Braimp.WebApi.Endpoints.Courses.UpdateCourse;
public class UpdateCourseDto : IMapWith<UpdateCourseCommand>
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public GradingSystem GradingSystem { get; set; }
    public string? CoverImageUrl { get; set; }
    public string? BackgroundColor { get; set; }
    public string? LogoUrl { get; set; }
    public Guid? CourseCategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateCourseDto, UpdateCourseCommand>();
    }
}
