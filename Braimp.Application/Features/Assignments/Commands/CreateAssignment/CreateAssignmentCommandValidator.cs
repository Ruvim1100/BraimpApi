using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Assignments.Commands.CreateAssignment;
public class CreateAssignmentCommandValidator : AbstractValidator<CreateAssignmentCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public CreateAssignmentCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(assignment => assignment.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters");

        RuleFor(assignment => assignment.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(assignment => assignment.Deadline)
            .Must(BeAFutureDate).WithMessage("Deadline must be a future date");

        RuleFor(assignment => assignment.CourseId)
            .NotEmpty().WithMessage("CourseId is required")
            .NotEqual(Guid.Empty).WithMessage("CourseId cannot be empty");

        RuleFor(assignment => assignment)
            .MustAsync(CourseExists).WithMessage("Specified course does not exist");
    }

    private bool BeAFutureDate(DateTimeOffset deadline)
    {
        return deadline > DateTimeOffset.UtcNow;
    }

    private async Task<bool> CourseExists(CreateAssignmentCommand command, CancellationToken cancellationToken)
    {
        return await _dbContext.Courses
            .AnyAsync(course => course.Id == command.CourseId, cancellationToken);
    }
}
