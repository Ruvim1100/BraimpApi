using MediatR;
using System.Text;

namespace Braimp.Application.Features.CourseImages.Commands.CreateCourseImage;
public class CreateCourseImageCommand : IRequest<Guid>
{
    public Guid CourseId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public Stream FileStream { get; set; } = null!;
    public Encoding Encoding { get; set; } = null!;
}
