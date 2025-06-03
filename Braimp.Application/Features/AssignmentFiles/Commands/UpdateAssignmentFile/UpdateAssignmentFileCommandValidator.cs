using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.AssignmentFiles.Commands.UpdateAssignmentFile;
public class UpdateAssignmentFileCommandValidator : AbstractValidator<UpdateAssignmentFileCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public UpdateAssignmentFileCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("AssignmentFileId is required");

        RuleFor(command => command.AssignmentId)
            .NotEmpty()
            .WithMessage("AssignmentId is required");

        RuleFor(command => command)
            .MustAsync(AssignmentFileExists)
            .WithMessage("Assignment File doesn't exist");
    }

    private async Task<bool> AssignmentFileExists(UpdateAssignmentFileCommand command, CancellationToken cancellationToken) =>
        await _dbContext.AssignmentFiles.AnyAsync(assignmentFile => assignmentFile.Id == command.Id &&
        assignmentFile.AssignmentId == command.AssignmentId, cancellationToken);
}
