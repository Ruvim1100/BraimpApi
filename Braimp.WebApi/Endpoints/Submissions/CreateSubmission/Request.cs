using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Submissions.CreateSubmission;
public class Request
{
    [MaxLength(300)]
    public string Text { get; set; } = string.Empty;
    [Required(ErrorMessage = "Display Name is required.")]
    [MaxLength(50)]
    public string DisplayName { get; set; } = string.Empty;
    [Required(ErrorMessage = "File is required.")]
    public IFormFile File { get; set; } = null!;
}
