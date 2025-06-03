using Braimp.Domain.Entities.Quizzes.Enums;
using MediatR;

namespace Braimp.Application.Features.QuizQuestions.Commands.CreateQuizQuestion;
public class CreateQuizQuestionCommand : IRequest<Guid>
{
    public string Text { get; set; } = string.Empty;
    public QuestionType QuestionType { get; set; }
    public int Weight { get; set; } = 1;
    public Guid QuizId { get; set; }
    public Guid CourseId { get; set; }
    public ResourceModel? Resource { get; set; }
    public ICollection<QuizOptionModel>? QuizOptions { get; set; }
}
