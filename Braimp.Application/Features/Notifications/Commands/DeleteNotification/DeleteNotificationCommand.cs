using MediatR;

namespace Braimp.Application.Features.Notifications.Commands.DeleteNotification;
public class DeleteNotificationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
}
