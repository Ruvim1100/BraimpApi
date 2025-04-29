    using FluentValidation;

namespace Braimp.Application.Features.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(deleteCategoryCommand => deleteCategoryCommand.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Category ID must not be empty.");
    }
}
