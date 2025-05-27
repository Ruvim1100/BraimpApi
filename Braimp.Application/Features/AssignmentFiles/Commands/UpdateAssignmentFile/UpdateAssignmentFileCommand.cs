using MediatR;

namespace Braimp.Application.Features.AssignmentFiles.Commands.UpdateAssignmentFile;
public class UpdateAssignmentFileCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid AssignmentId { get; set; }
}
