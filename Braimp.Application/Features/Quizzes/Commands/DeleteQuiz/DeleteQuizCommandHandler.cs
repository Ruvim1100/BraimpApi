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

        dbContext.Quizzes.Remove(quiz);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
