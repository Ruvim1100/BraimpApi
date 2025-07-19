using MediatR;

namespace Braimp.Application.Features.LessonFiles.Commands.DeleteLessonFile;
public class DeleteLessonFileCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
}
