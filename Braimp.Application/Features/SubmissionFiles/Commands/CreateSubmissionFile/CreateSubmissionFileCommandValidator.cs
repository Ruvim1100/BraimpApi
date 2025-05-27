using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.SubmissionFiles.Commands.CreateSubmissionFile;
public class CreateSubmissionFileCommandValidator : AbstractValidator<CreateSubmissionFileCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public CreateSubmissionFileCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.SubmissionId)
            .NotEmpty().WithMessage("SubmissionId is required")
            .NotEqual(Guid.Empty).WithMessage("SubmissionId cannot be empty");

        RuleFor(command => command)
            .MustAsync(SubmissionExists).WithMessage("Submission doesn't exist");
    }

    private async Task<bool> SubmissionExists(CreateSubmissionFileCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Submissions.AnyAsync(submission => 
        submission.Id == command.SubmissionId, cancellationToken);
}
