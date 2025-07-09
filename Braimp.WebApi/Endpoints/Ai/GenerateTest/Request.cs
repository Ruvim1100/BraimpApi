using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Ai.GenerateTest;
public class Request
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Language { get; set; } = string.Empty;

    [Required]
    public int QuestionCount { get; set; }

    [Required]
    [MinLength(100)]
    [MaxLength(5000)]
    public string SourceText { get; set; } = string.Empty;
}