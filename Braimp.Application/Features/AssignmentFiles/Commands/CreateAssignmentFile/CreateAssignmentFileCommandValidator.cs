using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.AssignmentFiles.Commands.CreateAssignmentFile;
public class CreateAssignmentFileCommandValidator : AbstractValidator<CreateAssignmentFileCommand>
{
    private readonly IBraimpDbContext _dbContext;
    private readonly ICourseAuthorizationService _courseAuthorization;
    private readonly ICurrentUserService _currentUser;

    public CreateAssignmentFileCommandValidator(IBraimpDbContext dbContext, 
        ICourseAuthorizationService courseAuthorization, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _courseAuthorization = courseAuthorization;
        _currentUser = currentUser;

        RuleFor(command => command.AssignmentId)
            .NotEmpty().WithMessage("AssignmentId is required");

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is required");

        RuleFor(command => command.DisplayName)
            .NotEmpty().WithMessage("Display name is required")
            .MaximumLength(255).WithMessage("Display name must be less than 255 characters");

        RuleFor(command => command.FileStream)
            .NotNull().WithMessage("File stream is required")
            .Must(stream => stream.CanRead && stream.Length > 0)
            .WithMessage("File stream must be readable and not empty");

        RuleFor(command => command.FileStream.Length)
            .LessThanOrEqualTo(10 * 1024 * 1024)
            .WithMessage("File size must be less than 10MB.");

        RuleFor(command => command)
            .MustAsync(AssignmentExists)
            .WithMessage("Assignemnt doesn't exist");

        RuleFor(x => x)
            .MustAsync(async (command, ct) =>
            {
                return await courseAuthorization.HasRole(
                    command.CourseId,
                    currentUser.UserId,
                    CourseRole.Owner);
            })
            .WithMessage("You must be the owner of the course to perform this action.");
    }

    private async Task<bool> AssignmentExists(CreateAssignmentFileCommand command, CancellationToken cancellationToken) => 
        await _dbContext.Assignments.AnyAsync(assignment => assignment.Id == command.AssignmentId &&
        assignment.CourseId == command.CourseId, cancellationToken);
}
