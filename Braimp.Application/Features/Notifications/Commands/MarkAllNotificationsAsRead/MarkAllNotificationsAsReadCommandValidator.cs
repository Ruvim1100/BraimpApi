using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Notifications.Commands.MarkAllNotificationsAsRead;
public class MarkAllNotificationsAsReadCommandValidator : AbstractValidator<MarkAllNotificationsAsReadCommand>
{
    private readonly IBraimpDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;
    public MarkAllNotificationsAsReadCommandValidator(IBraimpDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("Course Id cannot be empty");

        RuleFor(command => command)
            .MustAsync(CourseExists)
            .WithMessage("Course does not exist or you are not a participant.");
    }

    private async Task<bool> CourseExists(MarkAllNotificationsAsReadCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Courses
        .AnyAsync(course => course.Id == command.CourseId &&
        course.Participants.Any(participant => participant.UserId == _currentUserService.UserId));
}
