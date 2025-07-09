using Braimp.Domain.Entities.Quizzes.Enums;
using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.QuizQuestions.CreateQuizQuestion;
public class Request
{
    [Required]
    [MaxLength(1000)]
    public string Text { get; set; } = string.Empty;

    [Required]
    [EnumDataType(typeof(QuestionType))]
    public QuestionType QuestionType { get; set; }

    [Range(1, 100)]
    public int Weight { get; set; } = 1;

    public IEnumerable<OptionModel>? QuizOptions { get; set; }
}
