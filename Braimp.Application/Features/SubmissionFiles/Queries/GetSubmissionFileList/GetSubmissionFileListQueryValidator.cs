using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.SubmissionFiles.Queries.GetSubmissionFileList;
public class GetSubmissionFileListQueryValidator : AbstractValidator<GetSubmissionFileListQuery>
{
    private readonly IBraimpDbContext _dbContext;

    public GetSubmissionFileListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.SubmissionId)
            .NotEmpty().WithMessage("SubmissionId is required")
            .NotEqual(Guid.Empty).WithMessage("SubmissionId cannot be empty");

        RuleFor(command => command.AssignmentId)
            .NotEmpty().WithMessage("AssignmentId is required")
            .NotEqual(Guid.Empty).WithMessage("AssignmentId cannot be empty");

        RuleFor(query => query)
            .MustAsync(SubmissionExists)
            .WithMessage("Submission doesn't exist");
    }

    private async Task<bool> SubmissionExists(GetSubmissionFileListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Submissions.AnyAsync(submission => submission.Id == query.SubmissionId &&
        submission.AssignmentId == query.AssignmentId, cancellationToken);
}
