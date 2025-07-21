using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.ReviewCourse;
public class ReviewCourseCommandValidator : AbstractValidator<ReviewCourseCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public ReviewCourseCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is Required");

        RuleFor(command => command.Status)
            .Must(status => status == CourseStatus.Approved || status == CourseStatus.Rejected)
            .WithMessage("CourseStatus must be either Published or Rejected");

        RuleFor(command => command)
            .MustAsync(CourseExists);
    }

    private async Task<bool> CourseExists(ReviewCourseCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == command.CourseId, cancellationToken);
}
