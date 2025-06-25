using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.Courses.Commands.UpdateCourse;
public class UpdateCourseCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    ILogger<UpdateCourseCommandHandler> logger) : IRequestHandler<UpdateCourseCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Starting UpdateCourseCommand handling: CourseId={CourseId}, HasTitle={HasTitle}, HasCategory={HasCategory}",
            request.Id,
            request.Title is not null,
            request.CourseCategoryId.HasValue);

        var course = await dbContext.Courses
            .FirstAsync(course => course.Id == request.Id, cancellationToken);

        if (request.CourseCategoryId.HasValue)
            course.CourseCategoryId = request.CourseCategoryId.Value;

        if (request.Title is not null)
            course.Title = request.Title;

        if (request.Description is not null)
            course.Description = request.Description;

        if (request.GradingSystem.HasValue)
            course.GradingSystem = request.GradingSystem.Value;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "UpdateCourseCommand completed successfully: CourseId={CourseId}",
            request.Id);

        return Unit.Value;
    }
}
