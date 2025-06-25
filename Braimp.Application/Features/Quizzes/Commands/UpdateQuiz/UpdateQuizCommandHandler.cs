using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Quizzes.Commands.UpdateQuiz;
public class UpdateQuizCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateQuizCommand, Guid>
{
    public async Task<Guid> Handle(UpdateQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await dbContext.Quizzes
            .FirstAsync(quiz => quiz.Id == request.Id);

        if (request.Title != null)
            quiz.Title = request.Title;

        if (request.Description != null)
            quiz.Description = request.Description;

        if (request.TimeLimitMinutes.HasValue)
            quiz.TimeLimitMinutes = request.TimeLimitMinutes.Value;

        if (request.IsPublished.HasValue)
            quiz.IsPublished = request.IsPublished.Value;

        if (request.MaxAttempts.HasValue)
            quiz.MaxAttempts = request.MaxAttempts.Value;

        if (request.IsRandomized.HasValue)
            quiz.IsRandomized = request.IsRandomized.Value;

        if (request.StartTime.HasValue)
            quiz.AvailableFrom = request.StartTime.Value;

        dbContext.Quizzes.Update(quiz);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return quiz.Id;
    }
}
