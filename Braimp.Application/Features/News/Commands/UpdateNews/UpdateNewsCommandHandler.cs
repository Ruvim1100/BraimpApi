using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.News.Commands.UpdateNews;
public class UpdateNewsCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork,
    IBlobStorageService blobStorageService) : IRequestHandler<UpdateNewsCommand, Guid>
{
    public async Task<Guid> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
    {
        var news = await dbContext.CourseNews
            .FirstAsync(news => news.Id == request.Id,
            cancellationToken);

        if (!string.IsNullOrWhiteSpace(request.Title))
            news.Title = request.Title;

        if (!string.IsNullOrWhiteSpace(request.Content))
            news.Content = request.Content;

        if (request.FileStream is not null)
        {
            var resource = await dbContext.Resources
                .FirstAsync(resource => resource.Id == news.ImageResourceId, 
                cancellationToken);

            var extension = Path.GetExtension(request.OriginalFileName);
            var uniqueBlobName = $"{Guid.NewGuid()}{extension}";

            await blobStorageService.DeleteAsync(BlobContainers.News, resource.Url, cancellationToken);

            resource.Name = request.FileDisplayName + extension;
            resource.Url = uniqueBlobName;

            await blobStorageService.UploadAsync(request.FileStream, BlobContainers.News, uniqueBlobName, request.Encoding, cancellationToken);
            dbContext.Resources.Update(resource);
        }

        dbContext.CourseNews.Update(news);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return news.Id;
    }
}
