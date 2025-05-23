using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public DeleteCategoryCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Category ID must not be empty.");

        RuleFor(command => command)
            .MustAsync(CategoryExists)
            .WithMessage("Category was not found.");
    }
    private async Task<bool> CategoryExists(DeleteCategoryCommand command, CancellationToken cancellationToken) =>
        await _dbContext.CourseCategories.AnyAsync(category => category.Id == command.Id);
}
