using MediatR;
using System.Text;

namespace Braimp.Application.Features.LessonFiles.Commands.CreateLessonFile;

public class CreateLessonFileCommand : IRequest<Unit>
{
    public Guid LessonId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
    public Stream FileStream { get; set; } = null!;
    public Encoding? Encoding { get; set; } = null;
}
