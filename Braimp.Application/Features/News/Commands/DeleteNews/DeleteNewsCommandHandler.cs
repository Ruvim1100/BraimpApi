using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.News.Commands.DeleteNews;
public class DeleteNewsCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteNewsCommand>
{
    public async Task Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
    {
        var news = await dbContext.CourseNews
            .FirstAsync(news => news.Id == request.Id, cancellationToken);

        dbContext.CourseNews.Remove(news);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
