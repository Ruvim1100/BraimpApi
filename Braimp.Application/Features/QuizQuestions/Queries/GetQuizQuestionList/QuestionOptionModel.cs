namespace Braimp.Application.Features.QuizQuestions.Queries.GetQuizQuestionList;
public class QuestionOptionModel
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }

}
