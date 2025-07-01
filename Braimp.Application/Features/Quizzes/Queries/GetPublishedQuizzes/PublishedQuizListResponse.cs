namespace Braimp.Application.Features.Quizzes.Queries.GetPublishedQuizzes;
public class PublishedQuizListResponse
{
    public IList<PublishedQuizLookupModel> Quizzes { get; set; } = new List<PublishedQuizLookupModel>();
}
