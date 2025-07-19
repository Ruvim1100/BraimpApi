using MediatR;

namespace Braimp.Application.Features.Notifications.Commands.DeleteAllNotifications;
public class DeleteAllNotificationsCommand : IRequest<Unit>
{
    public Guid CourseId { get; set; }
}
