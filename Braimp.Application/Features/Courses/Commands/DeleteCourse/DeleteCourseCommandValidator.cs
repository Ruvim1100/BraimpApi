using FluentValidation;

namespace Braimp.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
    {
        public DeleteCourseCommandValidator() 
        {
            RuleFor(deleteCourseCommand => deleteCourseCommand.Id)
                .NotEqual(Guid.Empty);
            RuleFor(deleteCourseCommand => deleteCourseCommand.OwnerId)
                .NotEqual(Guid.Empty);
        }
    }
}
