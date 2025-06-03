using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Quizzes;

namespace Braimp.Application.Features.Quizzes.Queries.GetQuizzes;
public class QuizLookupModel : IMapWith<Quiz>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid CourseId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Quiz, QuizLookupModel>();
    }
}
