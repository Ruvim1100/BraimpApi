using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.Courses.Commands.DeleteCourse;
public class DeleteCourseCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    ILogger<DeleteCourseCommandHandler> logger) : IRequestHandler<DeleteCourseCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Starting DeleteCourseCommand handling: CourseId={CourseId}",
            request.Id);
        var course = await dbContext.Courses
            .FirstAsync(course => course.Id == request.Id, cancellationToken);

        if (course.IsDeleted)
        {
            logger.LogWarning("Course already marked as deleted: CourseId={CourseId}", request.Id);
            return Unit.Value;
        }

        course.IsDeleted = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        course.Status = CourseStatus.Archived;

        logger.LogInformation(
            "DeleteCourseCommand completed successfully: course with Id={CourseId} deleted",
            request.Id);

        return Unit.Value;
    }
}
