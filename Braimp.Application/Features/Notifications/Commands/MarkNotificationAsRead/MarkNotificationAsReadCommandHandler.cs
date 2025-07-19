using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Notifications.Commands.MarkNotificationAsRead;
public class MarkNotificationAsReadCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<MarkNotificationAsReadCommand, Unit>
{
    public async Task<Unit> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        var notification = await dbContext.Notifications
            .FirstAsync(notification => notification.Id == request.Id,
            cancellationToken);

        notification.IsRead = true;

        dbContext.Notifications.Update(notification);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
