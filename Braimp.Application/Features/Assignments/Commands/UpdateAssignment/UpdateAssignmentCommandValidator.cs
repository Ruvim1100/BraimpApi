using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Assignments.Commands.UpdateAssignment;
public class UpdateAssignmentCommandValidator : AbstractValidator<UpdateAssignmentCommand>
{
    private readonly IBraimpDbContext _dbContext;
    private readonly ICourseAuthorizationService _courseAuthorization;
    private readonly ICurrentUserService _currentUser;

    public UpdateAssignmentCommandValidator(IBraimpDbContext dbContext,
        ICourseAuthorizationService courseAuthorization, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _courseAuthorization = courseAuthorization;
        _currentUser = currentUser;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Assignment Id is required.");

        RuleFor(command => command.Title)
            .MaximumLength(100)
            .WithMessage("Title must not exceed 100 characters");

        RuleFor(command => command.Description)
            .MaximumLength(1000)
            .WithMessage("Description must not exceed 1000 characters");

        RuleFor(command => command.Deadline)
            .Must(BeAFutureDate).When(x => x.Deadline.HasValue)
            .WithMessage("Deadline must be a future date");

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");

        RuleFor(x => x)
            .MustAsync(AssignmentExistsInCourse)
            .WithMessage("The assignment does not exist in the specified course.");

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

    private bool BeAFutureDate(DateTimeOffset? deadline)
    {
        return deadline.HasValue && deadline > DateTimeOffset.UtcNow;
    }

    private async Task<bool> AssignmentExistsInCourse(UpdateAssignmentCommand command, CancellationToken cancellationToken)
    {
        return await _dbContext.Assignments
            .AnyAsync(assignment => assignment.Id == command.Id && 
            assignment.CourseId == command.CourseId, cancellationToken);
    }
}