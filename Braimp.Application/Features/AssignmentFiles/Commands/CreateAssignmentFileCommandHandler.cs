using MediatR;

namespace Braimp.Application.Features.AssignmentFiles.Commands;
public class CreateAssignmentFileCommandHandler : IRequestHandler<CreateAssignmentFileCommand, Guid>
{
    public Task<Guid> Handle(CreateAssignmentFileCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
