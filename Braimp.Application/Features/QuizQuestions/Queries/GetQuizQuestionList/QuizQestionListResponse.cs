namespace Braimp.Application.Features.QuizQuestions.Queries.GetQuizQuestionList;
public class QuizQestionListResponse
{
    public IList<QuizQuestionLookupModel> Questions { get; set; } 
        = new List<QuizQuestionLookupModel>();
}
