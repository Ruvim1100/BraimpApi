using AutoMapper;
using Braimp.Application.Features.Courses.Commands.UpdateCourse;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Courses.Enums;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Courses.UpdateCourse;
public class Request : IMapWith<UpdateCourseCommand>
{
    [Required]
    public Guid Id { get; set; }

    [MaxLength(100)]
    public string? Title { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    [EnumDataType(typeof(GradingSystem))]
    public GradingSystem GradingSystem { get; set; }

    [Url]
    [MaxLength(1000)]
    public string? CoverImageUrl { get; set; }
    public string? BackgroundColor { get; set; }
    [Url]
    [MaxLength(1000)]
    public string? LogoUrl { get; set; }
    public Guid? CourseCategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, UpdateCourseCommand>();
    }
}
