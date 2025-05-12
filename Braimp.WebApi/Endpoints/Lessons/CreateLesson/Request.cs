using AutoMapper;
using Braimp.Application.Features.Lessons.Commands.CreateLesson;
using Braimp.Application.Mapping;

namespace Braimp.WebApi.Endpoints.Lessons.CreateLesson;
public class Request : IMapWith<CreateLessonCommand>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsPublished { get; set; } = false;
    public int SortIndex { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, CreateLessonCommand>();
    }
}
