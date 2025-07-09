using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Quizzes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.QuizAttempts.Commands.CreateQuizAttempt;
public class CreateQuizAttemptCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    ICurrentUserService currentUserService) : IRequestHandler<CreateQuizAttemptCommand, QuizAttemptCreatedModel>
{
    public async Task<QuizAttemptCreatedModel> Handle(CreateQuizAttemptCommand request, CancellationToken cancellationToken)
    {
        int attemptsCount = await dbContext.QuizAttempts
            .CountAsync(attempt =>
                attempt.StudentId == currentUserService.UserId &&
                attempt.QuizId == request.QuizId,
                cancellationToken);

        var quizEntity = await dbContext.Quizzes
            .FirstAsync(quiz => quiz.Id == request.QuizId,
            cancellationToken);

        var quizAttempt = new QuizAttempt
        {
            Id = Guid.NewGuid(),
            StudentId = currentUserService.UserId,
            StartedAt = DateTimeOffset.UtcNow,
            TimeLimitMinutes = quizEntity.TimeLimitMinutes,
            IsPublished = false,
            AttemptNumber = attemptsCount + 1,
            QuizId = request.QuizId
        };

        dbContext.QuizAttempts.Add(quizAttempt);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new QuizAttemptCreatedModel
        {
            QuizAttemptId = quizAttempt.Id,
            StartedAt = quizAttempt.StartedAt,
            TimeLimitMinutes = quizAttempt.TimeLimitMinutes,
            AttemptNumber = quizAttempt.AttemptNumber
        };
    }
}