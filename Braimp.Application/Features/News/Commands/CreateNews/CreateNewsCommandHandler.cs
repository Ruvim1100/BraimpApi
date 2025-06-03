using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses;
using MediatR;

namespace Braimp.Application.Features.News.Commands.CreateNews;
public class CreateNewsCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<CreateNewsCommand, Guid>
{
    public async Task<Guid> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
    {
        var news = new CourseNews
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Content = request.Content,
            AuthorId = currentUserService.UserId,
            CourseId = request.CourseId
        };

        dbContext.CourseNews.Add(news);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return news.Id;
    }
}
