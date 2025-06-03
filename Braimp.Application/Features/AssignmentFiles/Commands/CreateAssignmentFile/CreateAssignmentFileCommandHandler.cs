using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Domain.Entities;
using Braimp.Domain.Entities.Assignments;
using MediatR;

namespace Braimp.Application.Features.AssignmentFiles.Commands.CreateAssignmentFile;
public class CreateAssignmentFileCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    IBlobStorageService blobStorageService) : IRequestHandler<CreateAssignmentFileCommand, Guid>
{
    public async Task<Guid> Handle(CreateAssignmentFileCommand request, CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(request.OriginalFileName);
        var uniqueBlobName = $"{Guid.NewGuid()}{extension}";

        await blobStorageService.UploadAsync(
            request.FileStream,
            containerName: BlobContainers.Assignments,
            blobName: uniqueBlobName,
            encoding: request.Encoding,
            cancellationToken);

        var resource = new Resource
        {
            Id = Guid.NewGuid(),
            Name = request.DisplayName,
            Url = uniqueBlobName
        };

        var assignmentFile = new AssignmentFile
        { 
            Id = Guid.NewGuid(), 
            AssignmentId = request.AssignmentId,
            ResourceId = resource.Id,
        };

        dbContext.AssignmentFiles.Add(assignmentFile);
        dbContext.Resources.Add(resource);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return assignmentFile.Id;
    }
}
