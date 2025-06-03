using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Notifications.Queries.GetNotifications;
public class GetNotificationListQueryHandler(IBraimpDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
    : IRequestHandler<GetNotificationListQuery, NotificationListResponse>
{
    public async Task<NotificationListResponse> Handle(GetNotificationListQuery request, CancellationToken cancellationToken)
    {
        var notifications = await dbContext.Notifications
            .Where(notification => notification.CourseId == request.CourseId &&
            notification.UserId == currentUserService.UserId)
            .ProjectTo<NotificationLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new NotificationListResponse { Notifications = notifications };
    }
}
