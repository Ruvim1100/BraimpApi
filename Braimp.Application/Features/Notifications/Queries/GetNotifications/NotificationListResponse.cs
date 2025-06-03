namespace Braimp.Application.Features.Notifications.Queries.GetNotifications;
public class NotificationListResponse
{
    public List<NotificationLookupModel> Notifications { get; set; } = new();
}
