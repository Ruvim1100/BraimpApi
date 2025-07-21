using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.CreateCourse;
public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public CreateCourseCommandValidator(IBraimpDbContext dbContext) 
    {
        _dbContext = dbContext;

        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Title cannot be empty")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(command => command.Description)
            .MaximumLength(1000)
            .WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(command => command.CourseCategoryId)
            .NotEmpty()
            .WithMessage("Category is required.");

        RuleFor(command => command)
            .MustAsync(CategoryExists)
            .WithMessage("Category Doesn't exist");
    }

    private async Task<bool> CategoryExists(CreateCourseCommand command, CancellationToken cancellationToken) =>
        await _dbContext.CourseCategories.AnyAsync(category => category.Id == command.CourseCategoryId, 
            cancellationToken);
}
