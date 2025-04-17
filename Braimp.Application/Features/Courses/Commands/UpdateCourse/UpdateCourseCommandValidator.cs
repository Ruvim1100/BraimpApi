using FluentValidation;

namespace Braimp.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator() 
        {
            RuleFor(updateCourseCommand => updateCourseCommand.Id)
                .NotEqual(Guid.Empty);
            RuleFor(updateCourseCommand => updateCourseCommand.OwnerId)
                .NotEqual(Guid.Empty);
            RuleFor(updateCourseCommand => updateCourseCommand.Title)
                .MaximumLength(100);
            RuleFor(updateCourseCommand => updateCourseCommand.Description)
                .MaximumLength(1000);
            RuleFor(updateCourseCommand => updateCourseCommand.CourseCategoryId)
                .Must(courseCategoryId => !courseCategoryId.HasValue || courseCategoryId.Value != Guid.Empty);
        }
    }
}
