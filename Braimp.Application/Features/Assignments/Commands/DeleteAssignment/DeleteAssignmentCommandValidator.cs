using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Assignments.Commands.DeleteAssignment;
public class DeleteAssignmentCommandValidator : AbstractValidator<DeleteAssignmentCommand>
{
    private readonly IBraimpDbContext _dbContext;
    private readonly ICourseAuthorizationService _courseAuthorization;
    private readonly ICurrentUserService _currentUser;
    public DeleteAssignmentCommandValidator(IBraimpDbContext dbContext,
        ICourseAuthorizationService courseAuthorization, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _courseAuthorization = courseAuthorization;
        _currentUser = currentUser;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("AssignmentId is required");

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");

        RuleFor(command => command)
            .MustAsync(AssignmentExists)
            .WithMessage("Specified assignment in the given course does not exist");

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

    private async Task<bool> AssignmentExists(DeleteAssignmentCommand command, CancellationToken cancellationToken) => 
        await _dbContext.Assignments
        .AnyAsync(assignment => assignment.Id == command.Id && 
        assignment.CourseId == command.CourseId, cancellationToken);
}
