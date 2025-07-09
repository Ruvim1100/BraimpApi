using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Domain.Entities;
using Braimp.Domain.Entities.Assignments;
using MediatR;

namespace Braimp.Application.Features.Submissions.Commands.CreateSubmission;
public class CreateSubmissionCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    IBlobStorageService blobStorageService, ICurrentUserService currentUserService) 
    : IRequestHandler<CreateSubmissionCommand, Guid>
{
    public async Task<Guid> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(request.OriginalFileName);
        var uniqueBlobName = $"{Guid.NewGuid()}{extension}";

        await blobStorageService.UploadAsync(
            request.FileStream,
            containerName: BlobContainers.Submissions,
            blobName: uniqueBlobName,
            encoding: request.Encoding,
            cancellationToken);

        var resource = new Resource
        {
            Id = Guid.NewGuid(),
            Name = request.DisplayName + extension,
            Url = uniqueBlobName
        };

        var submission = new Submission
        {
            Id = Guid.NewGuid(),
            Text = request.Text,
            CanEdit = false,
            AssignmentId = request.AssignmentId,
            StudentId = currentUserService.UserId,
            FileResourceId = resource.Id
        };

        dbContext.Resources.Add(resource);
        dbContext.Submissions.Add(submission);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return submission.Id;
    }
}
