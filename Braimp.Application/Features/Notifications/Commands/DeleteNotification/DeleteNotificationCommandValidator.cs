using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Notifications.Commands.DeleteNotification;
public class DeleteNotificationCommandValidator : AbstractValidator<DeleteNotificationCommand>
{
    private readonly IBraimpDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;
    public DeleteNotificationCommandValidator(IBraimpDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Notification Id is Required");

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is Required");

        RuleFor(command => command)
            .MustAsync(NotificationExists)
            .WithMessage("Notification does not exist or you do not have permission to delete it.");
    }

    private async Task<bool> NotificationExists(DeleteNotificationCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Notifications.AnyAsync(notification => notification.Id == command.Id &&
        notification.CourseId == command.CourseId && 
        notification.UserId == _currentUserService.UserId);

}
