using AutoMapper;
using Braimp.Application.Features.Quizzes.Commands.UpdateQuiz;
using Braimp.Application.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Quizzes.UpdateQuiz;
public class Request : IMapWith<UpdateQuizCommand>
{
    [MaxLength(100)]
    public string? Title { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Range(1, 240)]
    public int? TimeLimitMinutes { get; set; }

    public bool? IsPublished { get; set; }

    [Range(1, 10)]
    public int? MaxAttempts { get; set; }

    public bool? IsRandomized { get; set; }

    public DateTimeOffset? AvailableFrom { get; set; }
    public DateTimeOffset? AvailableUntil { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, UpdateQuizCommand>();
    }
}
