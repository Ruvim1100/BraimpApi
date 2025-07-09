using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Quizzes.Commands.DeleteQuiz;
public class DeleteQuizCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteQuizCommand>
{
    public async Task Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await dbContext.Quizzes
            .FirstAsync(quiz => quiz.Id == request.Id, cancellationToken);

        var courseId = quiz.CourseId;
        var deletedSortIndex = quiz.SortIndex;

        dbContext.Quizzes.Remove(quiz);

        var quizzesToUpdate = await dbContext.Quizzes
            .Where(quiz => quiz.CourseId == courseId && quiz.SortIndex > deletedSortIndex)
            .ToListAsync(cancellationToken);

        foreach (var quizToUpdate in quizzesToUpdate)
        {
            quizToUpdate.SortIndex -= 1;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
