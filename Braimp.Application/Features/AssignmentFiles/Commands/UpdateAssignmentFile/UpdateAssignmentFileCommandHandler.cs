using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.AssignmentFiles.Commands.UpdateAssignmentFile;
public class UpdateAssignmentFileCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateAssignmentFileCommand, Guid>
{
    public async Task<Guid> Handle(UpdateAssignmentFileCommand request, CancellationToken cancellationToken)
    {
        var assignmentFile = await dbContext.AssignmentFiles
            .FirstAsync(assignmentFile => assignmentFile.Id == request.Id, cancellationToken);

        var resource = await dbContext.Resources
            .FirstAsync(resource => resource.Id == assignmentFile.ResourceId, cancellationToken);

        resource.Name = request.Name;

        dbContext.Resources.Update(resource);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return assignmentFile.Id;
    }
}
