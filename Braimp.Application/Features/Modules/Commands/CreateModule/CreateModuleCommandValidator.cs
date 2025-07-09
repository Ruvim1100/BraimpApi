using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Commands.CreateModule;
public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public CreateModuleCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(createModuleCommand => createModuleCommand.Title)
            .NotEmpty().WithMessage("Module title is required.")
            .MaximumLength(100).WithMessage("Module title must not exceed 100 characters.");

        RuleFor(createModuleCommand => createModuleCommand.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required.");

        RuleFor(command => command)
            .MustAsync(CourseExists)
            .WithMessage("Course doesn't exist");
    }

    private Task<bool> CourseExists(CreateModuleCommand command, CancellationToken cancellationToken) =>
                 _dbContext.Courses.AnyAsync(course => course.Id == command.CourseId, cancellationToken);
}
