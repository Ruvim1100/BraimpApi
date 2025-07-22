namespace Braimp.Application.Features.QuizAttempts.Queries.GetQuizAttemptList;
public class QuizAttemptListResponse
{
    public List<QuizAttemptLookupModel> QuizAttempts { get; set; } = new List<QuizAttemptLookupModel>();
}
