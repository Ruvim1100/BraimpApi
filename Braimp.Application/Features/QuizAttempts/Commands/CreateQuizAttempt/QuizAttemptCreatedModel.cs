namespace Braimp.Application.Features.QuizAttempts.Commands.CreateQuizAttempt;
public class QuizAttemptCreatedModel
{
    public Guid QuizAttemptId { get; set; }
    public DateTimeOffset StartedAt { get; set; }
    public int? TimeLimitMinutes { get; set; }
    public int AttemptNumber { get; set; }
}
