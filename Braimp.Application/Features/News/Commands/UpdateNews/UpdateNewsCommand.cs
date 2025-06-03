using MediatR;

namespace Braimp.Application.Features.News.Commands.UpdateNews;
public class UpdateNewsCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Content { get; set; } = string.Empty;
    public Guid CourseId { get; set; }
}
