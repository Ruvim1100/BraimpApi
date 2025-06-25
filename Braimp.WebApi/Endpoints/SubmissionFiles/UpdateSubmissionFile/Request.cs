using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.SubmissionFiles.UpdateSubmissionFile;
public record Request(
    [Required]
    [MaxLength(100)]
    string Name
);