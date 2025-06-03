using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Quizzes.Commands.CreateQuiz;
public class CreateQuizCommandValidator : AbstractValidator<CreateQuizCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public CreateQuizCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");

        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title length cannot be more than 100 characters");

        RuleFor(command => command.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(command => command.TimeLimitMinutes)
            .GreaterThan(0).WithMessage("Time limit must be greater than 0 minutes")
            .LessThanOrEqualTo(240).WithMessage("Time limit cannot exceed 240 minutes")
            .When(x => x.TimeLimitMinutes.HasValue);

        RuleFor(command => command.MaxAttempts)
            .GreaterThan(0).WithMessage("MaxAttempts must be greater than 0")
            .LessThanOrEqualTo(10).WithMessage("MaxAttempts cannot exceed 10");

        RuleFor(command => command.StartTime)
            .Must(BeInTheFuture)
            .WithMessage("Start time must be in the future")
            .When(x => x.StartTime.HasValue);

        RuleFor(command => command)
            .MustAsync(CourseExists)
            .WithMessage("Course doesn't exist");
    }

    private async Task<bool> CourseExists(CreateQuizCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == command.CourseId, cancellationToken);

    private bool BeInTheFuture(DateTimeOffset? startTime) => 
        startTime.HasValue && startTime.Value > DateTimeOffset.UtcNow;

}
