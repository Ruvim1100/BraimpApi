using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Quizzes;

namespace Braimp.Application.Features.QuizAttempts.Queries.GetQuizAttemptList;
public class QuizAttemptLookupModel : IMapWith<QuizAttempt>
{
    public decimal? Score { get; set; }
    public int AttemptNumber { get; set; }
    public DateTimeOffset? FinishedAt { get; set; }
    public int? CorrectAnswerCount { get; set; }
    public int? IncorrectAnswerCount { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<QuizAttempt, QuizAttemptLookupModel>();
    }
}
