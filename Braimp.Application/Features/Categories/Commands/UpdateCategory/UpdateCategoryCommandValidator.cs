using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public UpdateCategoryCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Category ID must be provided.");

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Category name is required.")
            .MaximumLength(100)
            .WithMessage("Title must not exceed 100 characters.");

        RuleFor(command => command)
            .MustAsync(CategoryExists)
            .WithMessage("Category was not found.");
    }
    private async Task<bool> CategoryExists(UpdateCategoryCommand command, CancellationToken cancellationToken) =>
       await _dbContext.CourseCategories.AnyAsync(category => category.Id == command.Id, cancellationToken);
}
