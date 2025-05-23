using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionDetails;
public class GetSubmissionDetailsQueryValidator : AbstractValidator<GetSubmissionDetailsQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetSubmissionDetailsQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Submission ID is required")
            .NotEqual(Guid.Empty).WithMessage("Submission ID cannot be empty");

        RuleFor(command => command.AssignmentId)
            .NotEmpty().WithMessage("Assignment ID is required")
            .NotEqual(Guid.Empty).WithMessage("Assignment ID cannot be empty");

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("Course ID is required")
            .NotEqual(Guid.Empty).WithMessage("Course ID cannot be empty");

        RuleFor(command => command)
            .MustAsync(SubmissionExists).WithMessage("Submission doesn't exist");
    }

    private async Task<bool> SubmissionExists(GetSubmissionDetailsQuery command, CancellationToken cancellationToken) =>
        await _dbContext.Submissions
        .AnyAsync(submission => submission.Id == command.Id &&
        submission.AssignmentId == command.AssignmentId
        && submission.Assignment.CourseId == command.CourseId, cancellationToken);
}
