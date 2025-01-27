using Braimp.Application.Common.Exceptions;
using Braimp.Application.Interfaces;
using Braimp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Unit>
    {
        private readonly IBraimpDbContext _dbContext;

        public UpdateCourseCommandHandler(IBraimpDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _dbContext.Courses
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (course == null)
            {
                throw new NotFoundException(nameof(Course), request.Id);
            }

            if (course.OwnerId != request.OwnerId)
            {
                throw new UnauthorizedAccessException(
                    $"User {request.OwnerId} is not the owner of the course {course.Id}.");
            }

            course.Title = request.Title;
            course.Description = request.Description;

            if (request.CourseCategoryId.HasValue)
            {
                var categoryExists = await _dbContext.CourseCategories
                    .AnyAsync(cc => cc.Id == request.CourseCategoryId.Value, cancellationToken);

                if (!categoryExists)
                {
                    throw new NotFoundException(nameof(CourseCategory), request.CourseCategoryId);
                }

                course.CourseCategoryId = request.CourseCategoryId.Value;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
