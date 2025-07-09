using MediatR;
using System.Text;

namespace Braimp.Application.Features.Submissions.Commands.CreateSubmission;
public class CreateSubmissionCommand : IRequest<Guid>
{
    public string? Text { get; set; }
    public Guid AssignmentId { get; set; }
    public Guid CourseId { get; set; }
    public Stream FileStream { get; set; } = null!;
    public string OriginalFileName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public Encoding? Encoding { get; set; }
}
