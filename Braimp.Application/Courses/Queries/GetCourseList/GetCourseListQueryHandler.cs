using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Courses.Queries.GetCourseList
{
    public class GetCourseListQueryHandler : IRequestHandler<GetCourseListQuery, CourseListVm>
    {
        private readonly IBraimpDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCourseListQueryHandler(IBraimpDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CourseListVm> Handle(GetCourseListQuery request,  CancellationToken cancellationToken)
        {
            var courses = await _dbContext.Courses
                //.Include(c => c.CourseCategory)
                .ProjectTo<CourseLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CourseListVm { Courses = courses };
        }
    }
}
