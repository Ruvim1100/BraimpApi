using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.News.Commands.DeleteNews;
public class DeleteNewsCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork,
    IBlobStorageService blobStorageService) : IRequestHandler<DeleteNewsCommand, Unit>
{
    public async Task<Unit> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
    {
        var news = await dbContext.CourseNews
            .FirstAsync(news => news.Id == request.Id, cancellationToken);

        var resource = await dbContext.Resources
            .FirstOrDefaultAsync(resource => resource.Id == news.ImageResourceId,
            cancellationToken);

        if (resource != null)
        {
            await blobStorageService.DeleteAsync(BlobContainers.News, resource.Url, cancellationToken);
            dbContext.Resources.Remove(resource);
        }

        dbContext.CourseNews.Remove(news);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
