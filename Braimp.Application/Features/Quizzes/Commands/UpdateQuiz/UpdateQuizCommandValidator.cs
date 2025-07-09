using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Quizzes.Commands.UpdateQuiz;
public class UpdateQuizCommandValidator : AbstractValidator<UpdateQuizCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public UpdateQuizCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command)
            .MustAsync(QuizExists)
            .WithMessage("Quiz doesn't exist");

        When(c => c.Title != null, () => 
        {
            RuleFor(c => c.Title!)
                .NotEmpty().WithMessage("Title cannot be empty if provided")
                .MaximumLength(100).WithMessage("Title length cannot exceed 100 characters");
        });

        When(c => c.Description != null, () => 
        {
            RuleFor(c => c.Description!)
                .MaximumLength(1000)
                .WithMessage("Description cannot exceed 1000 characters");
        });

        When(c => c.TimeLimitMinutes.HasValue, () => 
        {
            RuleFor(c => c.TimeLimitMinutes!.Value)
                .GreaterThan(0).WithMessage("Time limit must be greater than 0 minutes")
                .LessThanOrEqualTo(240).WithMessage("Time limit cannot exceed 240 minutes");
        });

        When(c => c.AvailableFrom.HasValue && c.AvailableUntil.HasValue, () =>
        {
            RuleFor(c => c)
                .Must(c => c.AvailableUntil > c.AvailableFrom)
                .WithMessage("AvailableUntil must be later than AvailableFrom");
        });

    When(c => c.MaxAttempts.HasValue, () => 
        {
            RuleFor(c => c.MaxAttempts!.Value)
                .GreaterThan(0).WithMessage("MaxAttempts must be greater than 0")
                .LessThanOrEqualTo(10).WithMessage("MaxAttempts cannot exceed 10");
        });
    }

    private async Task<bool> QuizExists(UpdateQuizCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Quizzes.AnyAsync(quiz => quiz.Id == command.Id &&
        quiz.CourseId == command.CourseId, cancellationToken);
}
