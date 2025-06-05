using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.Courses.Commands.CreateCourse;
public class CreateCourseCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, ICurrentUserService currentUser,
    ILogger<CreateCourseCommandHandler> logger) : IRequestHandler<CreateCourseCommand, Guid>
{
    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Starting CreateCourseCommand handling: Title={Title}, CategoryId={CategoryId}, UserId={UserId}",
            request.Title,
            request.CourseCategoryId,
            currentUser.UserId);

        var course = new Course
        {
            Id = Guid.NewGuid(),
            OwnerId = currentUser.UserId,
            Title = request.Title,
            Description = request.Description,
            Status = CourseStatus.Pending,
            GradingSystem = request.GradingSystem,
            CourseCategoryId = request.CourseCategoryId 
        };

        dbContext.Courses.Add(course);

        course.Participants.Add(new CourseParticipant
        {
            Id = Guid.NewGuid(),
            UserId = currentUser.UserId,
            Role = CourseRole.Owner
        });

        logger.LogDebug(
            "Adding CourseParticipant for UserId={UserId} in CourseId={CourseId}",
            currentUser.UserId,
            course.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "CreateCourseCommand completed successfully: course created with Id={CourseId}",
            course.Id);

        return course.Id;
    }
}
