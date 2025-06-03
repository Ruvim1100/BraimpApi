using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Notifications.Queries.GetNotifications;
public class GetNotificationListQueryValidator : AbstractValidator<GetNotificationListQuery>
{
    private readonly IBraimpDbContext _dbContext;
    private readonly ICurrentUserService _currentUser;
    public GetNotificationListQueryValidator(IBraimpDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUser = currentUserService;

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("Course Id is required");

        RuleFor(command => command)
            .MustAsync(CourseExists)
            .WithMessage("Course doesn't exist or you are not a participant");
    }

    private async Task<bool> CourseExists(GetNotificationListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == query.CourseId &&
        course.Participants.Any(participant => participant.Id == _currentUser.UserId),
            cancellationToken);
}
