using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.News.Commands.UpdateNews;
public class UpdateNewsCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateNewsCommand, Guid>
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

        dbContext.CourseNews.Update(news);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return news.Id;
    }
}
