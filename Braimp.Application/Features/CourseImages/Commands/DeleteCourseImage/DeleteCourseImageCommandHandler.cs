using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.CourseImages.Commands.DeleteCourseImage;

public class DeleteCourseImageCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    IBlobStorageService blobStorageService) : IRequestHandler<DeleteCourseImageCommand>
{
    public async Task Handle(DeleteCourseImageCommand request, CancellationToken cancellationToken)
    {
        var courseImage = await dbContext.CourseImages
            .FirstAsync(image => image.Id == request.Id, cancellationToken);

        var resource = await dbContext.Resources
            .FirstAsync(resource => resource.Id == courseImage.ResourceId, cancellationToken);

        await blobStorageService.DeleteAsync(BlobContainers.Courses, resource.Url,cancellationToken);

        dbContext.CourseImages.Remove(courseImage);
        dbContext.Resources.Remove(resource);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
