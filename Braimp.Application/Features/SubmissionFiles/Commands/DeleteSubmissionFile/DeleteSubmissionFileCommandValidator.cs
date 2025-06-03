using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.SubmissionFiles.Commands.DeleteSubmissionFile;
public class DeleteSubmissionFileCommandValidator : AbstractValidator<DeleteSubmissionFileCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public DeleteSubmissionFileCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("SubmissionFileId is required");

        RuleFor(command => command.SubmissionId)
            .NotEmpty()
            .WithMessage("SubmissionId is required");
            
        RuleFor(command => command)
            .MustAsync(SubmissionExists)
            .WithMessage("SubmissionFile doesn't exist");
    }

    private async Task<bool> SubmissionExists(DeleteSubmissionFileCommand command, CancellationToken cancellationToken) =>
        await _dbContext.SubmissionFiles.AnyAsync(submissionFile => submissionFile.Id == command.Id &&
        submissionFile.SubmissionId == command.SubmissionId, cancellationToken);
}
