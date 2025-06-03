using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.AssignmentFiles.Queries.GetAssignmentFileList;
public class GetAssignmentFileListQueryValidator : AbstractValidator<GetAssignmentFileListQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetAssignmentFileListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.AssignmentId)
            .NotEmpty()
            .WithMessage("AssignmentId is required");

        RuleFor(query => query.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");

        RuleFor(query => query)
            .MustAsync(AssignmentExists)
            .WithMessage("Assignment was not found");
    }

    private async Task<bool> AssignmentExists(GetAssignmentFileListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Assignments.AnyAsync(assignment => assignment.Id == query.AssignmentId &&
        assignment.CourseId == query.CourseId, cancellationToken);
}
