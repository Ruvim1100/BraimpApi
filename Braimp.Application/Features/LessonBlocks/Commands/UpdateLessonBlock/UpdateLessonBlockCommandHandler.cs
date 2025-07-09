using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.LessonBlocks.Commands.UpdateLessonBlock;
public class UpdateLessonBlockCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) : IRequestHandler<UpdateLessonBlockCommand>
{
    public async Task Handle(UpdateLessonBlockCommand request, CancellationToken cancellationToken)
    {
        var lessonBlock = await dbContext.LessonBlocks
            .FirstAsync(block => block.Id == request.Id, cancellationToken);

        lessonBlock.Content = request.Content;

        dbContext.LessonBlocks.Update(lessonBlock);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
