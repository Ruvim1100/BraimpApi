using Braimp.Application.Abstraction;
using Braimp.Application.Common.Exceptions;
using Braimp.Domain.Entities.Courses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
        : IRequestHandler<UpdateCourseCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await dbContext.Courses
                .FirstOrDefaultAsync(course => course.Id == request.Id, cancellationToken);

            if (course == null)
                throw new NotFoundException(nameof(Course), request.Id);

            if (course.OwnerId != request.OwnerId)
                throw new UnauthorizedAccessException(
                    $"User {request.OwnerId} is not the owner of the course {course.Id}.");

            course.Title = request.Title;
            course.Description = request.Description;

            if (request.CourseCategoryId.HasValue)
            {
                var categoryExists = await dbContext.CourseCategories
                    .AnyAsync(courseCategory => courseCategory.Id == request.CourseCategoryId.Value, cancellationToken);

                if (!categoryExists)
                {
                    throw new NotFoundException(nameof(CourseCategory), request.CourseCategoryId);
                }

                course.CourseCategoryId = request.CourseCategoryId.Value;
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
