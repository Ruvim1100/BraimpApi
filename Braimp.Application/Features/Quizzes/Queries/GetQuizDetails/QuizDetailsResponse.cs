using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Quizzes;

namespace Braimp.Application.Features.Quizzes.Queries.GetQuizDetails;
public class QuizDetailsResponse : IMapWith<Quiz>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? TimeLimitMinutes { get; set; }
    public bool IsPublished { get; set; }
    public int MaxAttempts { get; set; }
    public bool IsRandomized { get; set; }
    public DateTimeOffset? StartTime { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid CourseId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Quiz, QuizDetailsResponse>();
    }
}
