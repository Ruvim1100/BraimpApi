using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.AssignmentFiles.Commands.DeleteAssignmentFile;
public class DeleteAssignmentFileCommandValidator : AbstractValidator<DeleteAssignmentFileCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public DeleteAssignmentFileCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;


        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("AssignmentFileId is required")
            .NotEqual(Guid.Empty).WithMessage("AssignmentFileId cannot be empty");

        RuleFor(command => command)
            .MustAsync(AssignmentFileExists).WithMessage("Assignment file not found");


    }

    private async Task<bool> AssignmentFileExists(DeleteAssignmentFileCommand command, CancellationToken cancellationToken) =>
        await _dbContext.AssignmentFiles.AnyAsync(assignmentFile => assignmentFile.Id == command.Id &&
        assignmentFile.AssignmentId == command.AssignmentId, cancellationToken);
}
