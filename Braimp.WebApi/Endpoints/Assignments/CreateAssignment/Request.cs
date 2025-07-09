using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Assignments.CreateAssignment;
public class Request
{
    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public DateTimeOffset? Deadline { get; set; }

}
