using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Assignments.Commands.CreateAssignment;
public class CreateAssignmentCommandValidator : AbstractValidator<CreateAssignmentCommand>
{
    private readonly IBraimpDbContext _dbContext;
    private readonly ICourseAuthorizationService _courseAuthorization;
    private readonly ICurrentUserService _currentUser;

    public CreateAssignmentCommandValidator(IBraimpDbContext dbContext,
        ICourseAuthorizationService courseAuthorization, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _courseAuthorization = courseAuthorization;
        _currentUser = currentUser;

        RuleFor(assignment => assignment.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters");

        RuleFor(assignment => assignment.Description)
            .MaximumLength(1000)
            .WithMessage("Description must not exceed 1000 characters");

        RuleFor(assignment => assignment.Deadline)
            .Must(d => !d.HasValue || d > DateTimeOffset.UtcNow)
            .WithMessage("Deadline must be a future date");

        RuleFor(assignment => assignment.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");

        RuleFor(assignment => assignment)
            .MustAsync(CourseExists)
            .WithMessage("Specified course does not exist");

        RuleFor(x => x)
            .MustAsync(async (command, ct) =>
            {
                return await courseAuthorization.HasRole(
                    command.CourseId,
                    currentUser.UserId,
                    CourseRole.Owner);
            })
            .WithMessage("You must be the owner of the course to perform this action.");
    }


    private async Task<bool> CourseExists(CreateAssignmentCommand command, CancellationToken cancellationToken)
    {
        return await _dbContext.Courses
            .AnyAsync(course => course.Id == command.CourseId, cancellationToken);
    }
}
