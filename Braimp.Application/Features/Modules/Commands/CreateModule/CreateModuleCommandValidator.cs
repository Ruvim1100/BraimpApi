using FluentValidation;

namespace Braimp.Application.Features.Modules.Commands.CreateModule;
public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
{
    public CreateModuleCommandValidator()
    {
        RuleFor(createModuleCommand => createModuleCommand.Title)
            .NotEmpty().WithMessage("Module title is required.")
            .MaximumLength(100).WithMessage("Module title must not exceed 100 characters.");

        RuleFor(createModuleCommand => createModuleCommand.Description)
            .MaximumLength(1000).WithMessage("Module description must not exceed 1000 characters.");

        RuleFor(createModuleCommand => createModuleCommand.CourseId)
            .NotEmpty().WithMessage("CourseId is required.")
            .NotEqual(Guid.Empty).WithMessage("CourseId must be a valid non-empty GUID.");

        RuleFor(createModuleCommand => createModuleCommand.SortIndex)
            .GreaterThanOrEqualTo(0).WithMessage("SortIndex must be greater than or equal to zero.");
    }
}
