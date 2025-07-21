using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.SystemStats.Queries;
public class GetSystemStatsQueryHandler(IBraimpDbContext dbContext) : IRequestHandler<GetSystemStatsQuery, SystemStatsResponse>
{
    public async Task<SystemStatsResponse> Handle(GetSystemStatsQuery request, CancellationToken cancellationToken)
    {
        var weekAgo = DateTimeOffset.Now.AddDays(-7);

        var totalCourses = await dbContext.Courses.
            CountAsync(cancellationToken);

        var totalPublishedCourses = await dbContext.Courses.
            CountAsync(course => course.Status == CourseStatus.Approved, 
            cancellationToken);

        var totalUsers = await dbContext.Users.
            CountAsync(cancellationToken);

        var newUsersLast7Days = await dbContext.Users.
            CountAsync(user => user.CreatedAt >= weekAgo, 
            cancellationToken);

        return new SystemStatsResponse 
        { 
            TotalCourses = totalCourses,
            PublishedCourses = totalPublishedCourses,
            TotalUsers = totalUsers,
            NewUsersLast7Days = newUsersLast7Days
        };
    }
}
