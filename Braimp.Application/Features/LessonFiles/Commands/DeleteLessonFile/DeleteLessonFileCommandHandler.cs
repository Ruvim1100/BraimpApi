using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.LessonFiles.Commands.DeleteLessonFile;
public class DeleteLessonFileCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    IBlobStorageService blobStorageService) : IRequestHandler<DeleteLessonFileCommand>
{
    public async Task Handle(DeleteLessonFileCommand request, CancellationToken cancellationToken)
    {
        var lessonFile = await dbContext.LessonFiles
            .FirstAsync(file => file.Id == request.Id, cancellationToken);

        var resource = await dbContext.Resources
            .FirstAsync(file => file.Id == lessonFile.ResourceId);

        await blobStorageService.DeleteAsync(BlobContainers.Lessons, resource.Url, cancellationToken);

        dbContext.Resources.Remove(resource);
        dbContext.LessonFiles.Remove(lessonFile);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
