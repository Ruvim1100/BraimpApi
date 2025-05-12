using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.LearningContent;
using MediatR;

namespace Braimp.Application.Features.Lessons.Commands.DeleteLesson;
public class DeleteLessonCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) : IRequestHandler<DeleteLessonCommand, Unit>
{
    public async Task<Unit> Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        var stub = new Lesson { Id = request.Id };

        dbContext.Lessons.Attach(stub);
        dbContext.Lessons.Remove(stub);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
