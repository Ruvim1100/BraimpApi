using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.AssignmentFiles.Commands.DeleteAssignmentFile;
internal class DeleteAssignmentFileCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork,
    IBlobStorageService blobStorageService) 
    : IRequestHandler<DeleteAssignmentFileCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAssignmentFileCommand request, CancellationToken cancellationToken)
    {
        var assignmentFile = await dbContext.AssignmentFiles
            .FirstAsync(assignmentFile => assignmentFile.Id == request.Id, cancellationToken);
        var resource = await dbContext.Resources
            .FirstAsync(resource => resource.Id == assignmentFile.ResourceId, cancellationToken);

        await blobStorageService.DeleteAsync(BlobContainers.Assignments, resource.Url, cancellationToken);

        dbContext.AssignmentFiles.Remove(assignmentFile);
        dbContext.Resources.Remove(resource);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
