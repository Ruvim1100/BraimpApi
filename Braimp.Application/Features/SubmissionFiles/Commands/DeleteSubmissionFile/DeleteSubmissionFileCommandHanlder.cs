using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.SubmissionFiles.Commands.DeleteSubmissionFile;
public class DeleteSubmissionFileCommandHanlder(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    IBlobStorageService blobStorageService) : IRequestHandler<DeleteSubmissionFileCommand>
{
    public async Task Handle(DeleteSubmissionFileCommand request, CancellationToken cancellationToken)
    {
        var submissionFile = await dbContext.SubmissionFiles
            .FirstAsync(submissionFile => submissionFile.Id == request.Id, cancellationToken);

        var resource = await dbContext.Resources
            .FirstAsync(resource => resource.Id == submissionFile.ResourceId, cancellationToken);

        await blobStorageService.DeleteAsync(BlobContainers.Submissions, resource.Url);

        dbContext.Resources.Remove(resource);
        dbContext.SubmissionFiles.Remove(submissionFile);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
