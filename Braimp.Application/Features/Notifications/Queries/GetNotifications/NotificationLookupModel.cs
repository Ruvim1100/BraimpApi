using AutoMapper;
using Braimp.Application.Mapping;
using Braimp.Domain.Entities.Notifications;
using Braimp.Domain.Entities.Notifications.Enums;

namespace Braimp.Application.Features.Notifications.Queries.GetNotifications;
public class NotificationLookupModel : IMapWith<Notification>
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public NotificationType Type { get; set; }
    public Guid CourseId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Notification, NotificationLookupModel>();
    }
}
