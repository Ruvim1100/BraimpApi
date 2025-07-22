using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.QuizAttempts.Commands.CreateQuizAttempt;
public class CreateQuizAttemptCommandValidator : AbstractValidator<CreateQuizAttemptCommand>
{
    private readonly IBraimpDbContext _dbContext;
    private readonly ICurrentUserService _currentUser;

    public CreateQuizAttemptCommandValidator(IBraimpDbContext dbContext, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is required");

        RuleFor(command => command.QuizId)
            .NotEmpty().WithMessage("QuizId is required");

        RuleFor(command => command)
            .MustAsync(QuizExsits);

        RuleFor(attempt => attempt.QuizId)
            .MustAsync(UserHasAttemptsLeft)
            .WithMessage("Maximum number of attempts reached for this quiz.");
    }

    private async Task<bool> QuizExsits(CreateQuizAttemptCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Quizzes.AnyAsync(quiz => quiz.Id == command.QuizId &&
        quiz.CourseId == command.CourseId,
            cancellationToken);

    private async Task<bool> UserHasAttemptsLeft(Guid quizId, CancellationToken cancellationToken)
    {
        var quiz = await _dbContext.Quizzes
            .Include(quiz => quiz.QuizAttempts)
            .FirstOrDefaultAsync(q => q.Id == quizId, cancellationToken);

        if (quiz == null || quiz.MaxAttempts == null)
            return true;

        var userId = _currentUser.UserId;

        var attemptsCount = await _dbContext.QuizAttempts
            .CountAsync(attempt => attempt.QuizId == quizId && attempt.StudentId == userId, cancellationToken);

        return attemptsCount < quiz.MaxAttempts;
    }
}
