using System.ComponentModel.DataAnnotations;

namespace Braimp.WebApi.Endpoints.Submissions.UpdateSubmission;
public record class Request([MaxLength(100)]string Text);

