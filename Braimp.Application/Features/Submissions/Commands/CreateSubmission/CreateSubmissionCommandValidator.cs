using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Submissions.Commands.CreateSubmission;
public class CreateSubmissionCommandValidator : AbstractValidator<CreateSubmissionCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public CreateSubmissionCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;


        RuleFor(command => command.AssignmentId)
            .NotEmpty().WithMessage("AssignmentId is required")
            .NotEqual(Guid.Empty).WithMessage("AssignmentId cannot be empty");

        RuleFor(command => command)
            .MustAsync(AssignmentExists).WithMessage("Assignment doesn't exist");

        RuleFor(command => command.StudentId)
            .NotEmpty().WithMessage("StudentId is required")
            .NotEqual(Guid.Empty).WithMessage("StudentId cannot be null");

        RuleFor(command => command.Text)
            .MaximumLength(500).WithMessage("The text cannot be more than 500 characters.");

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is required")
            .NotEqual(Guid.Empty).WithMessage("CourseId cannot be empty");

        //RuleFor(command => command)
        //    .MustAsync(NotPastDeadline).WithMessage("Cannot submit after assignment deadline");
    }

    private async Task<bool> AssignmentExists(CreateSubmissionCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Assignments.AnyAsync(assignment => assignment.Id == command.AssignmentId &&
        assignment.CourseId == command.CourseId, cancellationToken);

    //private async Task<bool> NotPastDeadline(CreateSubmissionCommand command, CancellationToken cancellationToken) 
    //{
    //    var assignment = await _dbContext.Assignments.AsNoTracking()
    //         .FirstAsync(assignment => assignment.Id == command.AssignmentId, cancellationToken);
    //    return DateTimeOffset.UtcNow <= assignment.Deadline;
    //}

}