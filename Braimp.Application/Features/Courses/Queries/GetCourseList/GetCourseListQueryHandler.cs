using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList
{
    public class GetCourseListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) : IRequestHandler<GetCourseListQuery, CourseListVm>
    {

        public async Task<CourseListVm> Handle(GetCourseListQuery request,  CancellationToken cancellationToken)
        {
            var courses = await dbContext.Courses
                //.Include(course => course.CourseCategory)
                .ProjectTo<CourseLookupDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CourseListVm { Courses = courses };
        }
    }
}
