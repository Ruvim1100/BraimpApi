using Braimp.Application.Abstraction;
using MediatR;
namespace Braimp.Application.Features.Lessons.Commands.UpdateLesson;
public class UpdateLessonCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateLessonCommand, Guid>
{
    public async Task<Guid> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await dbContext.Lessons.FindAsync(request.Id, cancellationToken);

        lesson.Title = request.Title ?? lesson.Title.Trim();
        lesson.Description = request.Description ?? lesson.Description;
        lesson.IsPublished = request.IsPublished ?? lesson.IsPublished;
        lesson.SortIndex = request.SortIndex ?? lesson.SortIndex;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return lesson.Id;
    }
}
