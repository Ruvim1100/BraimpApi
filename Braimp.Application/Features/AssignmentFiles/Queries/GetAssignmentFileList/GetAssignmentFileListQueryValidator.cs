using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.AssignmentFiles.Queries.GetAssignmentFileList;
public class GetAssignmentFileListQueryValidator : AbstractValidator<GetAssignmentFileListQuery>
{
    private readonly IBraimpDbContext _dbContext;
    private readonly ICourseAuthorizationService _courseAuthorization;
    private readonly ICurrentUserService _currentUser;
    public GetAssignmentFileListQueryValidator(IBraimpDbContext dbContext,
        ICourseAuthorizationService courseAuthorization, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _courseAuthorization = courseAuthorization;
        _currentUser = currentUser;

        RuleFor(query => query.AssignmentId)
            .NotEmpty()
            .WithMessage("AssignmentId is required");

        RuleFor(query => query.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");

        RuleFor(query => query)
            .MustAsync(AssignmentExists)
            .WithMessage("Assignment was not found");

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

    private async Task<bool> AssignmentExists(GetAssignmentFileListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Assignments.AnyAsync(assignment => assignment.Id == query.AssignmentId &&
        assignment.CourseId == query.CourseId, cancellationToken);
}
