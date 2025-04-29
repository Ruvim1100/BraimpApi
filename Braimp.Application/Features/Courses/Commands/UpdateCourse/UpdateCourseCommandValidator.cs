using FluentValidation;

namespace Braimp.Application.Features.Courses.Commands.UpdateCourse;
public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator() 
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Course ID must be provided.");

        RuleFor(x => x.Title)
            .MaximumLength(100)
            .WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.GradingSystem)
            .IsInEnum()
            .WithMessage("Invalid grading system specified.");

        RuleFor(x => x.CoverImageUrl)
            .MaximumLength(2048)
            .WithMessage("Cover image URL is too long.");

        RuleFor(x => x.LogoUrl)
            .MaximumLength(2048)
            .WithMessage("Logo URL is too long.");

        RuleFor(x => x.BackgroundColor)
            .MaximumLength(20)
            .WithMessage("Background color format is too long.");

        RuleFor(command => command.CourseCategoryId)
            .Must(command => !command.HasValue || command.Value != Guid.Empty)
            .WithMessage("Category ID must be a non-empty GUID if provided.");
    }
}
