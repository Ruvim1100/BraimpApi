using FluentValidation;

namespace Braimp.Application.Features.Categories.Commands.CreateCategory;
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(CreateCategoryCommand => CreateCategoryCommand.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
