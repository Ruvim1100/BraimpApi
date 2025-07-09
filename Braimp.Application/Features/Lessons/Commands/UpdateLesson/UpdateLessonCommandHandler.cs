using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Braimp.Application.Features.Lessons.Commands.UpdateLesson;
public class UpdateLessonCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateLessonCommand, Guid>
{
    public async Task<Guid> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await dbContext.Lessons
            .FirstAsync(lesson => lesson.Id == request.Id, 
            cancellationToken);

        if (request.Title != null)
            lesson!.Title = request.Title.Trim();

        if (request.Description != null)
            lesson!.Description = request.Description.Trim();

        lesson!.IsPublished = request.IsPublished ?? lesson.IsPublished;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return lesson.Id;
    }
}
