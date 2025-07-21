using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Ai.TranslateLesson;
public class Request
{
    [MaxLength(50)]
    public string Language { get; set; } = string.Empty;
}
