using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.QuizQuestions.CreateQuizQuestion;
public class OptionModel
{
    [Required]
    [MaxLength(300)]
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
