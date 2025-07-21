using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Submissions.Commands.GradeSubmission;
public class GradeSubmissionCommandValidator : AbstractValidator<GradeSubmissionCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public GradeSubmissionCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Submission ID is required");

        RuleFor(command => command.AssignmentId)
            .NotEmpty()
            .WithMessage("Assignment ID is required");

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("Course ID is required");

        RuleFor(command => command.ReviewComment)
            .MaximumLength(500)
            .WithMessage("Comment cannot be more than 500 characters");

        RuleFor(command => command)
            .MustAsync(SubmissionExists)
            .WithMessage("Submission doesn't exist");

    }
    private async Task<bool> SubmissionExists(GradeSubmissionCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Submissions
        .AnyAsync(submission => submission.Id == command.Id &&
        submission.AssignmentId == command.AssignmentId
        && submission.Assignment.CourseId == command.CourseId, cancellationToken);
}
