using Braimp.Application.Abstraction;
using Braimp.Application.Common.Exceptions;
using Braimp.Domain.Entities.Courses;
using MediatR;

namespace Braimp.Application.Features.Courses.Commands.DeleteCourse;
public class DeleteCourseCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteCourseCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await dbContext.Courses
            .FindAsync(new object[] {request.Id}, cancellationToken);

        if (course == null)
            throw new NotFoundException(nameof(Course), request.Id);

        if (request.OwnerId != course.OwnerId)
            throw new UnauthorizedAccessException(
                $"User {request.OwnerId} is not the owner of the course {course.Id}.");

        dbContext.Courses.Remove(course);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
