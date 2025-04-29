using FluentValidation;

namespace Braimp.Application.Features.Courses.Commands.CreateCourse;
public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator() 
    {
        RuleFor(createCourseCommand => createCourseCommand.Title)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(createCourseCommand => createCourseCommand.Description)
            .MaximumLength(1000);
        RuleFor(createCourseCommand => createCourseCommand.GradingSystem)
            .NotEmpty();
        RuleFor(createCourseCommand => createCourseCommand.CourseCategoryId)
            .NotEqual(Guid.Empty);
    }
}
