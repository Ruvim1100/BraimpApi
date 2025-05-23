using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Categories.Commands.CreateCategory;
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public CreateCategoryCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(command => command)
            .MustAsync(CategoryExists).WithMessage("This Category already exists");
    }

    private async Task<bool> CategoryExists(CreateCategoryCommand command, CancellationToken cancellationToken) =>
       !await _dbContext.CourseCategories
        .AnyAsync(category => category.Name.ToLower().Trim() == command.Name.ToLower().Trim(), cancellationToken);

}
