using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Lessons.Commands.CreateLesson;
public class CreateLessonCommandValidator : AbstractValidator<CreateLessonCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public CreateLessonCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Lesson Title is required")
            .MaximumLength(100).WithMessage("Lesson title must not exceed 100 characters.");

        RuleFor(command => command.Description)
            .MaximumLength(1000).WithMessage("Lesson Description must not exceed 1000 characters.");

        RuleFor(command => command.SortIndex)
            .GreaterThanOrEqualTo(0).WithMessage("Sort index must Greater Than Or EqualTo 0");

        RuleFor(command => command.ModuleId)
            .NotEmpty().WithMessage("ModuleId is required")
            .NotEqual(Guid.Empty).WithMessage("ModuleId must be a valid non-empty GUID.");

        RuleFor(command => command)
            .MustAsync(ModuleExists).WithMessage("Module doesn't  exist.");
    }

    private Task<bool> ModuleExists(CreateLessonCommand command, CancellationToken cancellationToken) =>
        _dbContext.Modules.AnyAsync(module => module.Id == command.ModuleId 
        && module.CourseId == command.CourseId, cancellationToken);
}
