using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.LearningContent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Lessons.Commands.CreateLesson;
public class CreateLessonCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) : IRequestHandler<CreateLessonCommand, Guid>
{
    public async Task<Guid> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var maxSortIndex = await dbContext.Lessons
            .Where(lesson => lesson.ModuleId == request.ModuleId)
            .MaxAsync(lesson => (int?)lesson.SortIndex, cancellationToken) ?? -1;

        var lesson = new Lesson
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            IsPublished = request.IsPublished,
            SortIndex = maxSortIndex + 1,
            ModuleId = request.ModuleId
        };

        dbContext.Lessons.Add(lesson);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return lesson.Id;
    }
}
