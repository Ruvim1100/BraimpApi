using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;

namespace Braimp.Application.Features.Courses.Commands.CreateCourse;
public class CreateCourseCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, ICurrentUserService currentUser) 
    : IRequestHandler<CreateCourseCommand, Guid>
{
    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
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

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return course.Id;
    }
}
