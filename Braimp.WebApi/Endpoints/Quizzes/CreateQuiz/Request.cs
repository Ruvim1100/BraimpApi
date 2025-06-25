using AutoMapper;
using Braimp.Application.Features.Quizzes.Commands.CreateQuiz;
using Braimp.Application.Mapping;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Quizzes.CreateQuiz;

public class Request : IMapWith<CreateQuizCommand>
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Range(1, 240)]
    public int? TimeLimitMinutes { get; set; }

    public bool IsPublished { get; set; }

    [Range(1, 10)]
    public int MaxAttempts { get; set; }

    public bool IsRandomized { get; set; }

    public DateTimeOffset? StartTime { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Request, CreateQuizCommand>();
    }
}
