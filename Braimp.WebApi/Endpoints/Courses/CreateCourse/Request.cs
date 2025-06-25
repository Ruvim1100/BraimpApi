using AutoMapper;
using Braimp.Application.Features.Courses.Commands.CreateCourse;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Courses.Enums;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Courses.CreateCourse;
public class Request : IMapWith<CreateCourseCommand>
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    [EnumDataType(typeof(GradingSystem))]
    public GradingSystem GradingSystem { get; set; }

    [Required]
    public Guid CourseCategoryId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, CreateCourseCommand>();
    }
}
