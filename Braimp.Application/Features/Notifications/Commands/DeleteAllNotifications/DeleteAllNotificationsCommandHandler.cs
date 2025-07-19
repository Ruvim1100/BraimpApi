using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Notifications.Commands.DeleteAllNotifications;
public class DeleteAllNotificationsCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    ICurrentUserService currentUserService) : IRequestHandler<DeleteAllNotificationsCommand, Unit>
{
    public async Task<Unit> Handle(DeleteAllNotificationsCommand request, CancellationToken cancellationToken)
    {
        var notifications = await dbContext.Notifications
            .Where(notification => notification.CourseId == request.CourseId &&
            notification.UserId == currentUserService.UserId)
            .ExecuteDeleteAsync(cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
