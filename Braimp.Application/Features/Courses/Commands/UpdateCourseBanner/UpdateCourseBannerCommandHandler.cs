using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.UpdateCourseBanner;
public class UpdateCourseBannerCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    IBlobStorageService blobStorageService) : IRequestHandler<UpdateCourseBannerCommand>
{
    public async Task Handle(UpdateCourseBannerCommand request, CancellationToken cancellationToken)
    {
        var course = await dbContext.Courses
            .FirstAsync(course => course.Id == request.Id, 
            cancellationToken);

        if (course.BannerResourceId.HasValue)
        {
            var oldResource = await dbContext.Resources
                .FirstOrDefaultAsync(resource => resource.Id == course.BannerResourceId.Value, cancellationToken);

            if (oldResource is not null)
            {
                await blobStorageService.DeleteAsync(BlobContainers.Courses, oldResource.Url, cancellationToken);
                dbContext.Resources.Remove(oldResource);
            }
        }

        var extension = Path.GetExtension(request.OriginalFileName);
        var uniqueBlobName = $"{Guid.NewGuid()}{extension}";

        await blobStorageService.UploadAsync(
            request.FileStream,
            containerName: BlobContainers.Courses,
            blobName: uniqueBlobName,
            encoding: request.Encoding,
            cancellationToken);

        var resource = new Resource
        {
            Id = Guid.NewGuid(),
            Name = request.DisplayName + extension,
            Url = uniqueBlobName
        };

        course.BannerResourceId = resource.Id;
        dbContext.Resources.Add(resource);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
