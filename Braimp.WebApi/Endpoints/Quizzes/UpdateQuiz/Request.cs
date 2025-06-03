using AutoMapper;
using Braimp.Application.Features.Quizzes.Commands.UpdateQuiz;
using Braimp.Application.Mapping;

namespace Braimp.WebApi.Endpoints.Quizzes.UpdateQuiz;
public class Request : IMapWith<UpdateQuizCommand>
{
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? TimeLimitMinutes { get; set; }
    public bool? IsPublished { get; set; }
    public int? MaxAttempts { get; set; }
    public bool? IsRandomized { get; set; }
    public DateTimeOffset? StartTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, UpdateQuizCommand>();
    }
}
