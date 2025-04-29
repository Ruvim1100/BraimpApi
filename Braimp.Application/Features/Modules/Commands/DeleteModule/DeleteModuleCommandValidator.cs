using FluentValidation;

namespace Braimp.Application.Features.Modules.Commands.DeleteModule;
public class DeleteModuleCommandValidator : AbstractValidator<DeleteModuleCommand>
{
    public DeleteModuleCommandValidator()
    {
        RuleFor(deleteModuleCommand => deleteModuleCommand.Id)
            .NotEmpty().WithMessage("Module id is required.")
            .NotEqual(Guid.Empty).WithMessage("Module id must be a valid non-empty GUID.");
    }
}
