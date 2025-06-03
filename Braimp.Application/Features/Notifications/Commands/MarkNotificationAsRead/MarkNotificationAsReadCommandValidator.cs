using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Notifications.Commands.MarkNotificationAsRead;
public class MarkNotificationAsReadCommandValidator : AbstractValidator<MarkNotificationAsReadCommand>
{
    private readonly IBraimpDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;
    public MarkNotificationAsReadCommandValidator(IBraimpDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Notification Id is required");

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("Course Id is required");

        RuleFor(command => command)
            .MustAsync(NotificationExists)
            .WithMessage("Notification doesn't exist or you don't have permission");
    }

    private async Task<bool> NotificationExists(MarkNotificationAsReadCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Notifications
        .AnyAsync(notification => notification.Id == command.Id &&
        notification.CourseId == command.CourseId &&
        notification.UserId == _currentUserService.UserId,
            cancellationToken);
}
