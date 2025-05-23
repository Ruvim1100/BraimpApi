using AutoMapper;
using Braimp.Application.Features.Lessons.Commands.UpdateLesson;
using Braimp.Application.Mapping;

namespace Braimp.WebApi.Endpoints.Lessons.UpdateLesson;
public class Request : IMapWith<UpdateLessonCommand>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsPublished { get; set; }
    public int? SortIndex { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, UpdateLessonCommand>().ReverseMap();
    }
}
