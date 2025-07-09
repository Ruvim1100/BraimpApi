using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.LessonBlocks.Commands.CreateLessonBlock;
public class CreateLessonBlockCommandValidator : AbstractValidator<CreateLessonBlockCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public CreateLessonBlockCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.LessonId)
            .NotEmpty().WithMessage("LessonId is required");

        RuleFor(command => command.Content)
            .NotEmpty().WithMessage("Content is Required")
            .MaximumLength(10000).WithMessage("Content cannot exceed 10000 characters");

        RuleFor(command => command.Type)
            .IsInEnum().WithMessage("Invalid block type");

        RuleFor(command => command)
            .MustAsync(LessonExists)
            .WithMessage("Lesson with given ID does not exist");
    }

    private async Task<bool> LessonExists(CreateLessonBlockCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Lessons.AnyAsync(lesson => lesson.Id == command.LessonId, cancellationToken);
}
