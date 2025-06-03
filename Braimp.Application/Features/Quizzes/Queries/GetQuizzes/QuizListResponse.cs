namespace Braimp.Application.Features.Quizzes.Queries.GetQuizzes;
public class QuizListResponse
{
    public List<QuizLookupModel> Quizzes { get; set; } = new();
}
