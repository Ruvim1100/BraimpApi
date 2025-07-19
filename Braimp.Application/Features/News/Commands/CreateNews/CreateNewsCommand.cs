using MediatR;
using System.Text;

namespace Braimp.Application.Features.News.Commands.CreateNews;
public class CreateNewsCommand : IRequest<Unit>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid CourseId { get; set; }
    public string FileDisplayName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public Stream FileStream { get; set; } = null!;
    public Encoding? Encoding { get; set; } = null;
}
