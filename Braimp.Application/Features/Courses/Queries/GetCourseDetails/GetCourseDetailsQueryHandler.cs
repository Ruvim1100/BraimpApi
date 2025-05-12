using AutoMapper;
using Braimp.Application.Abstraction;
using Braimp.Application.Exceptions;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Queries.GetCourseDetails;
public class GetCourseDetailsQueryHandler(IBraimpDbContext dbContext, IMapper _mapper,
    ICurrentUserService currentUser, ICourseAuthorizationService courseAuthorizationService) 
    : IRequestHandler<GetCourseDetailQuery, CourseDetailsResponse>
{

    public async Task<CourseDetailsResponse> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
    {
        await courseAuthorizationService.EnsureUserHasRole(
            request.Id, currentUser.UserId, CourseRole.Owner, CourseRole.Assistant);

        var course = await dbContext.Courses
            .Include(course => course.CourseCategory)
            .Include(course => course.Tags)
                .ThenInclude(courseTag => courseTag.Tag)
            .FirstOrDefaultAsync(item => item.Id == request.Id, cancellationToken);

        if (course is null)
            throw new NotFoundException(nameof(Course), request.Id);


        return _mapper.Map<CourseDetailsResponse>(course);
    }
}
