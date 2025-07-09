using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.QuizAttempts.Commands.CreateQuizAttempt;
public class CreateQuizAttemptCommandValidator : AbstractValidator<CreateQuizAttemptCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public CreateQuizAttemptCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is required");

        RuleFor(command => command.QuizId)
            .NotEmpty().WithMessage("QuizId is required");

        RuleFor(command => command)
            .MustAsync(QuizExsits);
    }

    private async Task<bool> QuizExsits(CreateQuizAttemptCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Quizzes.AnyAsync(quiz => quiz.Id == command.QuizId &&
        quiz.CourseId == command.CourseId,
            cancellationToken);
}
