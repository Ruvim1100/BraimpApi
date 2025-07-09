using AutoMapper;
using Braimp.Application.Features.Courses.Commands.UpdateCourse;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Courses.Enums;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Courses.UpdateCourse;
public class Request : IMapWith<UpdateCourseCommand>
{
    [MaxLength(100)]
    public string? Title { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public GradingSystem? GradingSystem { get; set; }
    public CourseStatus? Status { get; set; }
    public Guid? CourseCategoryId { get; set; }

    public List<Guid>? TagIds { get; set; } = new();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, UpdateCourseCommand>();
    }
}


   