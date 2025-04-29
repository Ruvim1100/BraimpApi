using FluentValidation;

namespace Braimp.Application.Features.Modules.Commands.UpdateModule;
public class UpdateModuleCommandValidator : AbstractValidator<UpdateModuleCommand>
{
    public UpdateModuleCommandValidator()
    {
        RuleFor(updateModuleCommand => updateModuleCommand.Id)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Module id is required.");

        RuleFor(updateModuleCommand => updateModuleCommand.Title)
            .MaximumLength(100).WithMessage("Module title must not exceed 100 characters.");

        RuleFor(updateModuleCommand => updateModuleCommand.Description)
            .MaximumLength(1000).WithMessage("Module description must not exceed 1000 characters.");

        RuleFor(updateModuleCommand => updateModuleCommand.SortIndex)
            .GreaterThanOrEqualTo(0)
            .When(updateModuleCommand => updateModuleCommand.SortIndex.HasValue)
            .WithMessage("SortIndex must be greater than or equal to zero.");
    }
}