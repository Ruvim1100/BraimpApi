using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Domain.Entities;
using MediatR;
using Braimp.Domain.Entities.LearningContent;

namespace Braimp.Application.Features.LessonFiles.Commands.CreateLessonFile;
public class CreateLessonFileCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    IBlobStorageService blobStorageService) : IRequestHandler<CreateLessonFileCommand, Unit>
{
    public async Task<Unit> Handle(CreateLessonFileCommand request, CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(request.OriginalFileName);
        var uniqueBlobName = $"{Guid.NewGuid()}{extension}";

        await blobStorageService.UploadAsync(
            request.FileStream,
            containerName: BlobContainers.Lessons,
            blobName: uniqueBlobName,
            encoding: request.Encoding,
            cancellationToken);

        var resource = new Resource
        {
            Id = Guid.NewGuid(),
            Name = request.DisplayName + extension,
            Url = uniqueBlobName
        };

        var lessonFile = new LessonFile
        {
            Id = Guid.NewGuid(),
            LessonId = request.LessonId,
            ResourceId = resource.Id,
        };

        dbContext.LessonFiles.Add(lessonFile);
        dbContext.Resources.Add(resource);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
