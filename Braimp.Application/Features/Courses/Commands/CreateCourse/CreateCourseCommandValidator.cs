using FluentValidation;

namespace Braimp.Application.Features.Courses.Commands.CreateCourse;
public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator() 
    {
        RuleFor(command => command.Title)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(command => command.Description)
            .MaximumLength(1000);
        RuleFor(command => command.GradingSystem)
            .NotEmpty();
        RuleFor(command => command.CourseCategoryId)
            .NotEqual(Guid.Empty);
    }
}
