using Braimp.Domain.Entities.Courses.Enums;

namespace Braimp.Application.Abstraction;
public interface ICourseAuthorizationService
{
    Task EnsureUserHasRole(Guid courseId, Guid userId, params CourseRole[] allowedRoles);
    Task EnsureUserIsCourseParticipant(Guid courseId, Guid userId);
    Task<bool> HasRole(Guid courseId, Guid userId, params CourseRole[] allowedRoles);
}
