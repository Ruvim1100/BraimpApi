using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.LearningContent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.LessonBlocks.Commands.CreateLessonBlock;
public class CreateLessonBlockCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateLessonBlockCommand>
{
    public async Task Handle(CreateLessonBlockCommand request, CancellationToken cancellationToken)
    {
        var maxSortIndex = await dbContext.LessonBlocks
            .Where(block => block.LessonId == request.LessonId)
            .MaxAsync(block => (int?)block.SortIndex, cancellationToken) ?? -1;

        var lessonBlock = new LessonBlock
        {
            Id = Guid.NewGuid(),
            LessonId = request.LessonId,
            BlockType = request.Type,
            Content = request.Content,
            SortIndex = maxSortIndex + 1
        };

        dbContext.LessonBlocks.Add(lessonBlock);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
