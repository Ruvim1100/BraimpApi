using MediatR;

namespace Braimp.Application.Features.Notifications.Commands.MarkNotificationAsRead;
public class MarkNotificationAsReadCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
}
