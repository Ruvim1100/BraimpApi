using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.LearningContent;
using MediatR;

namespace Braimp.Application.Features.Lessons.Commands.CreateLesson;
public class CreateLessonCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) : IRequestHandler<CreateLessonCommand, Guid>
{
    public async Task<Guid> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = new Lesson
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            IsPublished = request.IsPublished,
            SortIndex = request.SortIndex,
            ModuleId = request.ModuleId
        };

        dbContext.Lessons.Add(lesson);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return lesson.Id;
    }
}
