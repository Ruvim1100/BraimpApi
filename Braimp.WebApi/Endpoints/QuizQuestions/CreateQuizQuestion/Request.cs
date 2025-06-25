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

    [Required]
    public Guid QuizId { get; set; }

    [Required]
    public Guid CourseId { get; set; }
    public IFormFile? File { get; set; }

    [MaxLength(255)]
    public string? DisplayName { get; set; }
    public IEnumerable<OptionModel>? QuizOptions { get; set; }
}
