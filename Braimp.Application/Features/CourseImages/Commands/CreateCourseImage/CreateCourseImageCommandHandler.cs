using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Domain.Entities;
using Braimp.Domain.Entities.Courses;
using MediatR;

namespace Braimp.Application.Features.CourseImages.Commands.CreateCourseImage;
public class CreateCourseImageCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork,
    IBlobStorageService blobStorageService) : IRequestHandler<CreateCourseImageCommand, Guid>
{
    public async Task<Guid> Handle(CreateCourseImageCommand request, CancellationToken cancellationToken)
    {
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
            Name = request.DisplayName,
            Url = uniqueBlobName
        };

        var courseImage = new CourseImage
        {
            Id = Guid.NewGuid(),
            ResourceId = resource.Id,
            CourseId = request.CourseId,
        };

        dbContext.CourseImages.Add(courseImage);
        dbContext.Resources.Add(resource);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return courseImage.Id;
    }
}
