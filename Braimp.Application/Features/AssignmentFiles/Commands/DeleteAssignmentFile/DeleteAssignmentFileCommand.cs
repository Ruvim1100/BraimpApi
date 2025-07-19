using MediatR;

namespace Braimp.Application.Features.AssignmentFiles.Commands.DeleteAssignmentFile;
public class DeleteAssignmentFileCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
}
