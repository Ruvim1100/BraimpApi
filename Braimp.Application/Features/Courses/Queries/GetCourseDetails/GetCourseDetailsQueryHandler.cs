using AutoMapper;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.Courses.Queries.GetCourseDetails;
public class GetCourseDetailsQueryHandler(IBraimpDbContext dbContext, IMapper _mapper, ILogger<GetCourseDetailsQueryHandler> logger,
    ICurrentUserService currentUser) 
    : IRequestHandler<GetCourseDetailQuery, CourseDetailsResponse>
{

    public async Task<CourseDetailsResponse> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation(
                    "Starting GetCourseDetailQuery handling: CourseId={CourseId}, UserId={UserId}",
                    request.Id,
                    currentUser.UserId);


        var course = await dbContext.Courses
            .Include(course => course.CourseCategory)
            .Include(course => course.Tags)
                .ThenInclude(courseTag => courseTag.Tag)
            .FirstAsync(item => item.Id == request.Id, cancellationToken);

        logger.LogInformation(
            "GetCourseDetailQuery completed successfully: CourseId={CourseId}",
            request.Id);

        return _mapper.Map<CourseDetailsResponse>(course);
    }
}
