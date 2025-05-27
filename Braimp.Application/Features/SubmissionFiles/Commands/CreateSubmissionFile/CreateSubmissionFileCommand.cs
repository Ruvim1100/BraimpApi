using MediatR;
using System.Text;

namespace Braimp.Application.Features.SubmissionFiles.Commands.CreateSubmissionFile;
public class CreateSubmissionFileCommand : IRequest<Guid>
{
    public Guid SubmissionId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public Stream FileStream { get; set; } = null!;
    public Encoding? Encoding { get; set; } = null;
}
