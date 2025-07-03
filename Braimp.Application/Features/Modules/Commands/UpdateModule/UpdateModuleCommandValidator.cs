using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Commands.UpdateModule;
public class UpdateModuleCommandValidator : AbstractValidator<UpdateModuleCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public UpdateModuleCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(updateModuleCommand => updateModuleCommand.Id)
            .NotEmpty()
            .WithMessage("Module ID is required.");

        RuleFor(updateModuleCommand => updateModuleCommand.Title)
            .MaximumLength(100)
            .WithMessage("Module title must not exceed 100 characters.");

        RuleFor(updateModuleCommand => updateModuleCommand.SortIndex)
            .GreaterThanOrEqualTo(0)
            .When(updateModuleCommand => updateModuleCommand.SortIndex.HasValue)
            .WithMessage("SortIndex must be greater than or equal to zero.");

        RuleFor(command => command)
            .MustAsync(ModuleExists)
            .WithMessage("Module doesn't exists");
    }
    private Task<bool> ModuleExists(UpdateModuleCommand command, CancellationToken cancellationToken) =>
        _dbContext.Modules.AnyAsync(module => module.Id == command.Id
        && module.CourseId == command.CourseId);
}