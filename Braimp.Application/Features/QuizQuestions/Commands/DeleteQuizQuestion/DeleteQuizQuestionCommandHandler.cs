using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.QuizQuestions.Commands.DeleteQuizQuestion;
public class DeleteQuizQuestionCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteQuizQuestionCommand>
{
    public async Task Handle(DeleteQuizQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await dbContext.QuizQuestions
            .FirstAsync(question => question.Id == request.Id,
            cancellationToken);
        var quizId = question.QuizId;
        var deletedSortIndex = question.SortIndex;

        dbContext.QuizQuestions.Remove(question);

        var questionsToUpdate = await dbContext.QuizQuestions
            .Where(question => question.QuizId == quizId && question.SortIndex > deletedSortIndex)
            .ToListAsync(cancellationToken);

        foreach (var questionToUpdate in questionsToUpdate)
        {
            questionToUpdate.SortIndex -= 1;
        }


        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
