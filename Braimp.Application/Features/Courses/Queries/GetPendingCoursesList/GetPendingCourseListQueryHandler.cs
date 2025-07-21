using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Queries.GetPendingCoursesList;
public class GetPendingCourseListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) 
    : IRequestHandler<GetPendingCourseListQuery, PendingCourseListResponse>
{
    public async Task<PendingCourseListResponse> Handle(GetPendingCourseListQuery request, CancellationToken cancellationToken)
    {
        var courses = await dbContext.Courses
            .Where(course => course.Status == CourseStatus.Pending)
            .ProjectTo<PendingCourseLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        return new PendingCourseListResponse { Courses = courses };
    }
}
