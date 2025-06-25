using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Submissions.CreateSubmission;
public class Request
{
    [Required]
    [MaxLength(100)]
    public string Text { get; set; } = string.Empty;
}
