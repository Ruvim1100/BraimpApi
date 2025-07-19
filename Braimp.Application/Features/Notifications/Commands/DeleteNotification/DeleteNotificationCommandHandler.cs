using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Notifications.Commands.DeleteNotification;
public class DeleteNotificationCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteNotificationCommand, Unit>
{
    public async Task<Unit> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await dbContext.Notifications
            .FirstAsync(notification => notification.Id == request.Id);

        dbContext.Notifications.Remove(notification);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
