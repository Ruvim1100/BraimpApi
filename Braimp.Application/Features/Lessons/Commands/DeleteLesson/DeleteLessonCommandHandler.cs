using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Lessons.Commands.DeleteLesson;
public class DeleteLessonCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) : IRequestHandler<DeleteLessonCommand, Unit>
{
    public async Task<Unit> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await dbContext.Lessons
                    .FirstAsync(lesson => lesson.Id == request.Id, cancellationToken);

        var moduleId = lesson.ModuleId;
        var deletedSortIndex = lesson.SortIndex;
        
        dbContext.Lessons.Remove(lesson);

        var lessonsToUpdate = await dbContext.Lessons
            .Where(lesson => lesson.ModuleId == moduleId && lesson.SortIndex > deletedSortIndex)
            .ToListAsync(cancellationToken);

        foreach (var lessonToUpdate in lessonsToUpdate)
        {
            lessonToUpdate.SortIndex -= 1;
        }


        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
