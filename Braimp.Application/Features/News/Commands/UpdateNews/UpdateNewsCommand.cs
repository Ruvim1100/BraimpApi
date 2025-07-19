using MediatR;
using System.Text;

namespace Braimp.Application.Features.News.Commands.UpdateNews;
public class UpdateNewsCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public Guid CourseId { get; set; }
    public string? FileDisplayName { get; set; }
    public string? OriginalFileName { get; set; }
    public Stream? FileStream { get; set; } = null!;
    public Encoding? Encoding { get; set; } = null;
}
