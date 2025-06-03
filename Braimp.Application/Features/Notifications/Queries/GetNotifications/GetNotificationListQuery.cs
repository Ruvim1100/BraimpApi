using MediatR;

namespace Braimp.Application.Features.Notifications.Queries.GetNotifications;
public class GetNotificationListQuery : IRequest<NotificationListResponse>
{
    public Guid CourseId { get; set; }
}
