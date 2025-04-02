using Braimp.Application.Common.Exceptions;
using Braimp.Application.Interfaces;
using Braimp.Domain.Entities;
using Braimp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Guid>
    {
        private readonly IBraimpDbContext _dbContext;
        public CreateCourseCommandHandler(IBraimpDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var categoryExists = await _dbContext.CourseCategories
                .AnyAsync(c => c.Id == request.CourseCategoryId, cancellationToken);

            if (!categoryExists)
            { 
                throw new NotFoundException(nameof(CourseCategory), request.CourseCategoryId);
            }

            var course = new Course
            {
                Id = Guid.NewGuid(),
                OwnerId = request.OwnerId,
                Title = request.Title,
                Description = request.Description,
                Status = CourseStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                CourseCategoryId = request.CourseCategoryId 
            };

            await _dbContext.Courses.AddAsync(course, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return course.Id;
        }
    }
}
