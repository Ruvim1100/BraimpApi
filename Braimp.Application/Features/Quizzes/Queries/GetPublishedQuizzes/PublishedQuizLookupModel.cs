using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Quizzes;

namespace Braimp.Application.Features.Quizzes.Queries.GetPublishedQuizzes;
public class PublishedQuizLookupModel : IMapWith<Quiz>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int MaxAttempts { get; set; }

    public DateTimeOffset? AvailableFrom { get; set; }
    public DateTimeOffset? AvailableUntil { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Quiz, PublishedQuizLookupModel>();
    }
}
