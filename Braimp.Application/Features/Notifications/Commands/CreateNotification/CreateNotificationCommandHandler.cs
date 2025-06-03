using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Notifications;
using MediatR;

namespace Braimp.Application.Features.Notifications.Commands.CreateNotification;
public class CreateNotificationCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateNotificationCommand, Guid>
{
    public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Message = request.Message,
            IsRead = false,
            Type = request.Type,
            CourseId = request.CourseId
        };

        dbContext.Notifications.Add(notification);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return notification.Id;
    }
}
