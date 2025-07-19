using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Notifications.Commands.MarkAllNotificationsAsRead;
public class MarkAllNotificationsAsReadCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService) : IRequestHandler<MarkAllNotificationsAsReadCommand, Unit>
{
    public async Task<Unit> Handle(MarkAllNotificationsAsReadCommand request, CancellationToken cancellationToken)
    {
        var notifications = await dbContext.Notifications
            .Where(notification => notification.CourseId == request.CourseId &&
            notification.UserId == currentUserService.UserId)
            .ToListAsync(cancellationToken);

        dbContext.Notifications.UpdateRange(notifications);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
