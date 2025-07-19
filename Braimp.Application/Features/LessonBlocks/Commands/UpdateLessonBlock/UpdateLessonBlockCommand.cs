using MediatR;

namespace Braimp.Application.Features.LessonBlocks.Commands.UpdateLessonBlock;
public class UpdateLessonBlockCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
    public string Content { get; set; } = string.Empty;
}
