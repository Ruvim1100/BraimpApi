using FluentValidation;

namespace Braimp.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator() 
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Course ID must be provided.");

            RuleFor(command => command.OwnerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Owner ID must be provided.");

            RuleFor(command => command.Title)
                .MaximumLength(100)
                .When(command => command.Title != null)
                .WithMessage("Title must not exceed 100 characters.");

            RuleFor(command => command.Description)
                .MaximumLength(1000)
                .When(command => command.Description != null)
                .WithMessage("Description must not exceed 1000 characters.");

            RuleFor(command => command.GradingSystem)
                .IsInEnum()
                .When(c => c.GradingSystem.HasValue)
                .WithMessage("Invalid grading system specified.");

            RuleFor(command => command.CoverImageUrl)
                .Must(uri => uri == null || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(command => command.CoverImageUrl != null)
                .WithMessage("Invalid URL format for Cover Image.");

            RuleFor(command => command.LogoUrl)
                .Must(uri => uri == null || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(command => command.LogoUrl != null)
                .WithMessage("Invalid URL format for Logo.");

            RuleFor(command => command.CourseCategoryId)
                .Must(command => !command.HasValue || command.Value != Guid.Empty)
                .WithMessage("Category ID must be a non-empty GUID if provided.");
        }
    }
}
