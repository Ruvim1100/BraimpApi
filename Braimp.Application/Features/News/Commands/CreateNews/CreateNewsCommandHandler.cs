using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Domain.Entities;
using Braimp.Domain.Entities.Courses;
using MediatR;

namespace Braimp.Application.Features.News.Commands.CreateNews;
public class CreateNewsCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService, IBlobStorageService blobStorageService) : IRequestHandler<CreateNewsCommand, Unit>
{
    public async Task<Unit> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(request.OriginalFileName);
        var uniqueBlobName = $"{Guid.NewGuid()}{extension}";

        var news = new CourseNews
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Content = request.Content,
            AuthorId = currentUserService.UserId,
            CourseId = request.CourseId
        };

        await blobStorageService.UploadAsync(request.FileStream, BlobContainers.News, uniqueBlobName, request.Encoding, cancellationToken);

        var resource = new Resource
        {
            Id = Guid.NewGuid(),
            Name = request.FileDisplayName + extension,
            Url = uniqueBlobName
            
        };

        news.ImageResourceId = resource.Id;

        dbContext.Resources.Add(resource);
        dbContext.CourseNews.Add(news);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
