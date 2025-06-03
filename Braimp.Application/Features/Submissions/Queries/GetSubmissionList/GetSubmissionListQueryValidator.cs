using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionList;
public class GetSubmissionListQueryValidator: AbstractValidator<GetSubmissionListQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetSubmissionListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.AssignmentId)
            .NotEmpty()
            .WithMessage("Assignment ID is required");

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("Course ID is required");

        RuleFor(command => command)
            .MustAsync(AssignmentExists)
            .WithMessage("Assignment doesn't exist");
    }
    private async Task<bool> AssignmentExists(GetSubmissionListQuery command, CancellationToken cancellationToken) =>
        await _dbContext.Assignments.AnyAsync(assignment => assignment.Id == command.AssignmentId &&
        assignment.CourseId == command.CourseId, cancellationToken);
}
