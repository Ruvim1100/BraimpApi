using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Domain.Entities;
using Braimp.Domain.Entities.Assignments;
using MediatR;

namespace Braimp.Application.Features.SubmissionFiles.Commands.CreateSubmissionFile;
public class CreateSubmissionFileCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    IBlobStorageService blobStorageService) : IRequestHandler<CreateSubmissionFileCommand, Guid>
{
    public async Task<Guid> Handle(CreateSubmissionFileCommand request, CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(request.OriginalFileName);
        var uniqeBlobName = $"{Guid.NewGuid()}{extension}";

        await blobStorageService.UploadAsync(
            stream: request.FileStream,
            containerName: BlobContainers.Submissions,
            blobName: uniqeBlobName,
            encoding: request.Encoding);

        var resource = new Resource
        {
            Id = Guid.NewGuid(),
            Name = request.DisplayName,
            Url = uniqeBlobName
        };

        var submissionFile = new SubmissionFile
        {
            Id = Guid.NewGuid(),
            SubmissionId = request.SubmissionId,
            ResourceId = resource.Id
        };

        dbContext.Resources.Add(resource);
        dbContext.SubmissionFiles.Add(submissionFile);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return submissionFile.Id;
    }
}
