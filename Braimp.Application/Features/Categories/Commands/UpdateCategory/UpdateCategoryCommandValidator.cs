using FluentValidation;

namespace Braimp.Application.Features.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(updateCategoryCommand => updateCategoryCommand.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Category ID must be provided.");

        RuleFor(updateCategoryCommand => updateCategoryCommand.Name)
            .NotEmpty()
            .WithMessage("Category name is required.")
            .MaximumLength(100)
            .WithMessage("Title must not exceed 100 characters.");
    }
}
