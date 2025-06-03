using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Notifications.Commands.CreateNotification;
public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public CreateNotificationCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("CourseId cannot be empty");

        RuleFor(command => command)
            .MustAsync(CourseExists)
            .WithMessage("Course doesn't exist");

        RuleFor(command => command.Title)
            .NotEmpty().WithMessage("Title is Required")
            .MinimumLength(100).WithMessage("The Title cannot be longer than 100 characters.");

        RuleFor(command => command.Message)
            .NotEmpty().WithMessage("Message is required")
            .MaximumLength(300).WithMessage("The Title cannot ve longer than 300 characters");
    }

    private async Task<bool> CourseExists(CreateNotificationCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == command.CourseId);

}
