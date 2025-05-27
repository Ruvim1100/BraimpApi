using MediatR;
using System.Text;

namespace Braimp.Application.Features.AssignmentFiles.Commands.CreateAssignmentFile;
public class CreateAssignmentFileCommand : IRequest<Guid>
{
    public Guid CourseId { get; set; }
    public Guid AssignmentId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public Stream FileStream { get; set; } = null!;
    public Encoding? Encoding { get; set; } = null;
}
