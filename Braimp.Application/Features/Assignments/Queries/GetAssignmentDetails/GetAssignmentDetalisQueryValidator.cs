using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Assignments.Queries.GetAssignmentDetails;
public class GetAssignmentDetalisQueryValidator : AbstractValidator<GetAssignmentDetailsQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetAssignmentDetalisQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("AssignmentId is required");

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");

        RuleFor(command => command)
            .MustAsync(AssignmentExists)
            .WithMessage("Specified assignment in the given course does not exist");
    }
    private async Task<bool> AssignmentExists(GetAssignmentDetailsQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Assignments.AnyAsync(assignment => assignment.Id == query.Id &&
        assignment.CourseId == query.CourseId, cancellationToken);
}
