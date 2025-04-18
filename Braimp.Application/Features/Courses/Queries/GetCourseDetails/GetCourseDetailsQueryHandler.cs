using AutoMapper;
using Braimp.Application.Abstraction;
using Braimp.Application.Common.Exceptions;
using Braimp.Domain.Entities.Courses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Queries.GetCourseDetails;
public class GetCourseDetailsQueryHandler(IBraimpDbContext dbContext, IMapper _mapper) 
    : IRequestHandler<GetCourseDetailQuery, CourseDetailsResponse>
{

    public async Task<CourseDetailsResponse> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
    {
        var course = await dbContext.Courses
            .Include(course => course.CourseCategory)
            .Include(course => course.Tags)
                .ThenInclude(courseTag => courseTag.Tag)
            .FirstOrDefaultAsync(item => item.Id == request.Id, cancellationToken);

        if (course == null)
        {
            throw new NotFoundException(nameof(Course), request.Id);
        }

        if (course.OwnerId != request.OwnerId)
        {
            throw new UnauthorizedAccessException($"User {request.OwnerId} is not the owner of the course {course.Id}");
        } // in 

        return _mapper.Map<CourseDetailsResponse>(course);
    }
}
