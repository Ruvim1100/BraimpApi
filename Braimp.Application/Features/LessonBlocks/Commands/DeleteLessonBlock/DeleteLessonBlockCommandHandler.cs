using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.LessonBlocks.Commands.DeleteLessonBlock;
public class DeleteLessonBlockCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteLessonBlockCommand, Unit>
{
    public async Task<Unit> Handle(DeleteLessonBlockCommand request, CancellationToken cancellationToken)
    {
        var lessonBlock = await dbContext.LessonBlocks
            .FirstAsync(block => block.Id == request.Id, cancellationToken);

        dbContext.LessonBlocks.Remove(lessonBlock);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
