using MediatR;

namespace Braimp.Application.Features.Notifications.Commands.MarkAllNotificationsAsRead;
public class MarkAllNotificationsAsReadCommand : IRequest
{
    public Guid CourseId { get; set; }
}
