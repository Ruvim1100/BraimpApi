using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.EnrollmentRequests.Commands.CreateEnrollmentRequest;
public class CreateEnrollmentRequestCommandValidator : AbstractValidator<CreateEnrollmentRequestCommand>
{
    private readonly IBraimpDbContext _dbContext;
    private readonly ICurrentUserService _currentUser;

    public CreateEnrollmentRequestCommandValidator(IBraimpDbContext dbContext, ICurrentUserService currentUser)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is Required");

        RuleFor(command => command)
            .MustAsync(CourseExists).WithMessage("Course with the given ID does not exist."); ;

        RuleFor(command => command)
            .MustAsync(NotAlreadyRequested)
            .WithMessage("The enrollment request for this course already exists.");
    }

    private async Task<bool> CourseExists(CreateEnrollmentRequestCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == command.CourseId, cancellationToken);

    private async Task<bool> NotAlreadyRequested(CreateEnrollmentRequestCommand command, CancellationToken cancellationToken) =>
        !await _dbContext.EnrollmentRequests
            .AnyAsync(enrollment =>
                enrollment.CourseId == command.CourseId &&
                enrollment.UserId == _currentUser.UserId &&
                enrollment.Status == EnrollmentStatus.Pending,
                cancellationToken);
}
