using AutoMapper;
using Braimp.Application.Common.Exceptions;
using Braimp.Application.Interfaces;
using Braimp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailQuery, CourseDetailsVm>
    {
        private readonly IBraimpDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCourseDetailsQueryHandler(IBraimpDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CourseDetailsVm> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
        {
            var course = await _dbContext.Courses
                .Include(c => c.CourseCategory)
                .Include(c => c.Tags)
                    .ThenInclude(ct => ct.Tag)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (course == null)
            {
                throw new NotFoundException(nameof(Course), request.Id);
            }

            if (course.OwnerId != request.OwnerId)
            {
                throw new UnauthorizedAccessException($"User {request.OwnerId} is not the owner of the course {course.Id}");
            }

            return _mapper.Map<CourseDetailsVm>(course);
        }
    }
}
