using AutoMapper;
using Braimp.Application.Features.Lessons.Commands.UpdateLesson;
using Braimp.Application.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Lessons.UpdateLesson;
public class Request : IMapWith<UpdateLessonCommand>
{
    [MaxLength(100)]
    public string? Title { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public bool? IsPublished { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, UpdateLessonCommand>();
    }
}
