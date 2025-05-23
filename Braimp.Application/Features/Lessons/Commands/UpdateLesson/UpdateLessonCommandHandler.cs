using Braimp.Application.Abstraction;
using MediatR;
namespace Braimp.Application.Features.Lessons.Commands.UpdateLesson;
public class UpdateLessonCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateLessonCommand, Guid>
{
    public async Task<Guid> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await dbContext.Lessons.FindAsync(request.Id, cancellationToken);

        if (request.Title != null)
            lesson!.Title = request.Title.Trim();

        if (request.Description != null)
            lesson!.Description = request.Description.Trim();

        lesson!.IsPublished = request.IsPublished ?? lesson.IsPublished;
        lesson.SortIndex = request.SortIndex ?? lesson.SortIndex;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return lesson.Id;
    }
}
