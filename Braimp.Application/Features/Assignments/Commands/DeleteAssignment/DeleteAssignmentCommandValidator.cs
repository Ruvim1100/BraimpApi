using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Assignments.Commands.DeleteAssignment;
public class DeleteAssignmentCommandValidator : AbstractValidator<DeleteAssignmentCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public DeleteAssignmentCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("AssignmentId is required")
            .NotEqual(Guid.Empty).WithMessage("AssignmentId cannot be null");

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is required")
            .NotEqual(Guid.Empty).WithMessage("CourseId cannot be null");

        RuleFor(command => command)
            .MustAsync(AssignmentExists).WithMessage("Specified assignment in the given course does not exist");
    }

    private async Task<bool> AssignmentExists(DeleteAssignmentCommand command, CancellationToken cancellationToken) => 
        await _dbContext.Assignments
        .AnyAsync(assignment => assignment.Id == command.Id && 
        assignment.CourseId == command.CourseId, cancellationToken);
}
