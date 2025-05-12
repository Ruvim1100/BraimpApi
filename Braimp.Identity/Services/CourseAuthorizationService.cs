using Braimp.Application.Abstraction;
using Braimp.Application.Exceptions;
using Braimp.Domain.Entities.Courses.Enums;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Infrastructure.Identity;
public class CourseAuthorizationService(IBraimpDbContext dbContext) : ICourseAuthorizationService
{
    public async Task EnsureUserHasRole(Guid courseId, Guid userId, params CourseRole[] allowedRoles)
    {
        var hasAccess = await dbContext.CourseParticipants
            .AnyAsync(participant =>
            participant.CourseId == courseId &&
            participant.UserId == userId &&
            allowedRoles.Contains(participant.Role));

        if (!hasAccess)
            throw new ForbiddenAccessException(
                $"Access denied: User {userId} requires role(s) {string.Join(", ", allowedRoles)}");
    }

    public async Task EnsureUserIsCourseParticipant(Guid courseId, Guid userId)
    {
        var isParticipant = await dbContext.CourseParticipants
            .AnyAsync(p => p.CourseId == courseId && p.UserId == userId);

        if (!isParticipant)
            throw new ForbiddenAccessException($"Access denied: User {userId} is not a participant of course {courseId}");
    }
}
