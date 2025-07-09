using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Quizzes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Quizzes.Commands.CreateQuiz;
public class CreateQuizCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateQuizCommand, Guid>
{
    public async Task<Guid> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
    {

        var maxSortIndex = await dbContext.Quizzes
            .Where(quiz => quiz.CourseId == request.CourseId)
            .MaxAsync(quiz => (int?)quiz.SortIndex, cancellationToken) ?? -1;

        var quiz = new Quiz
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            TimeLimitMinutes = request.TimeLimitMinutes,
            IsPublished = request.IsPublished,
            MaxAttempts = request.MaxAttempts,
            IsRandomized = request.IsRandomized,
            SortIndex = maxSortIndex + 1,
            AvailableFrom = request.AvailableFrom,
            AvailableUntil = request.AvailableUntil,
            CourseId = request.CourseId
        };

        dbContext.Quizzes.Add(quiz);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return quiz.Id;
    }
}
