using Braimp.Application.Abstraction;
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

        logger.LogDebug(
            "Removing course: Id={CourseId}, Title={Title}",
            course.Id,
            course.Title);

        dbContext.Courses.Remove(course);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "DeleteCourseCommand completed successfully: course with Id={CourseId} deleted",
            request.Id);

        return Unit.Value;
    }
}
