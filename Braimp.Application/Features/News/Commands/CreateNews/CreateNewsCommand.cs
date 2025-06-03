using MediatR;

namespace Braimp.Application.Features.News.Commands.CreateNews;
public class CreateNewsCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid AuthorId { get; set; }
    public Guid CourseId { get; set; }
}
