using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Submissions.Commands.UpdateSubmission;
public class UpdateSubmissionCommandValidator : AbstractValidator<UpdateSubmissionCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public UpdateSubmissionCommandValidator(IBraimpDbContext dbContext)
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

        RuleFor(x => x.Text)
            .MaximumLength(500)
            .When(x => x.Text is not null)
            .WithMessage("Text is too long");

        RuleFor(command => command)
            .MustAsync(SubmissionExists)
            .WithMessage("Submission doesn't exist");
    }
    private async Task<bool> SubmissionExists(UpdateSubmissionCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Submissions
        .AnyAsync(submission => submission.Id == command.Id &&
        submission.AssignmentId == command.AssignmentId
        && submission.Assignment.CourseId == command.CourseId, cancellationToken);
}
