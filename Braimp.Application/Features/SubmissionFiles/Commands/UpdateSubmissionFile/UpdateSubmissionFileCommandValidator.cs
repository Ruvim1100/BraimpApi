using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.SubmissionFiles.Commands.UpdateSubmissionFile;
public class UpdateSubmissionFileCommandValidator : AbstractValidator<UpdateSubmissionFileCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public UpdateSubmissionFileCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("SubmissionFileId is required");

        RuleFor(command => command.SubmissionId)
            .NotEmpty()
            .WithMessage("SubmissionId is required");

        RuleFor(command => command)
            .MustAsync(SubmissionFileExists)
            .WithMessage("SubmissionFile doesn't exist");
    }

    private async Task<bool> SubmissionFileExists(UpdateSubmissionFileCommand command, CancellationToken cancellationToken) =>
        await _dbContext.SubmissionFiles.AnyAsync(submissionFile => submissionFile.Id == command.Id &&
        submissionFile.SubmissionId == command.SubmissionId);
}
