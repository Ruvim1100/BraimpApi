using MediatR;

namespace Braimp.Application.Features.LessonBlocks.Commands.DeleteLessonBlock;
public class DeleteLessonBlockCommand : IRequest<Unit>
{
    public Guid LessonId { get; set; }
    public Guid Id { get; set; }
}
