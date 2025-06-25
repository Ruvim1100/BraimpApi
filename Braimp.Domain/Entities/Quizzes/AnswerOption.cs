namespace Braimp.Domain.Entities.Quizzes;
public class AnswerOption
{
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public bool IsSelected { get; set; }

    public Guid AnswerId { get; set; }
    public AttemptAnswer AttemptAnswer { get; set; } = null!;
}
