using FluentValidation;

namespace Braimp.Application.Features.Courses.Commands.CreateCourse;
public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator() 
    {
        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Title cannot be empty")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(command => command.Description)
            .MaximumLength(1000)
            .WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(command => command.GradingSystem)
            .NotEmpty()
            .WithMessage("Grading system is required.");

        RuleFor(command => command.CourseCategoryId)
            .NotEmpty()
            .WithMessage("Category is required.");
    }
}
