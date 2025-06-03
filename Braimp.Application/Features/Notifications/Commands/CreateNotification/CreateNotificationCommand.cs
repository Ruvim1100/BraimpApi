using Braimp.Domain.Entities.Notifications.Enums;
using MediatR;

namespace Braimp.Application.Features.Notifications.Commands.CreateNotification;
public class CreateNotificationCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public NotificationType Type { get; set; }
    public Guid CourseId { get; set; }
}
