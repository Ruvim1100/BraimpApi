using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.LessonFiles.Commands.CreateLessonFile;
public class CreateLessonFileCommandValidator : AbstractValidator<CreateLessonFileCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public CreateLessonFileCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.LessonId)
            .NotEmpty().WithMessage("LessonId is required");

        RuleFor(command => command.DisplayName)
            .NotEmpty().WithMessage("Display name is required")
            .MaximumLength(255).WithMessage("Display name must be less than 255 characters");

        RuleFor(command => command.FileStream)
            .NotNull().WithMessage("File stream is required")
            .Must(stream => stream.CanRead && stream.Length > 0)
            .WithMessage("File stream must be readable and not empty");

        RuleFor(command => command)
            .MustAsync(LessonExists)
            .WithMessage("lesson doesn't exist");
    }

    private async Task<bool> LessonExists(CreateLessonFileCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Lessons.AnyAsync(lesson => lesson.Id == command.LessonId, cancellationToken);
}
