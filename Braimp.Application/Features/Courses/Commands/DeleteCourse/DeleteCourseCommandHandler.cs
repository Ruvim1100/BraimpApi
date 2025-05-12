using Braimp.Application.Abstraction;
using Braimp.Application.Exceptions;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.DeleteCourse;
public class DeleteCourseCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, ICurrentUserService currentUser) 
    : IRequestHandler<DeleteCourseCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var isOwner = await dbContext.CourseParticipants
            .AnyAsync(participant => participant.CourseId == request.Id &&
                           participant.UserId == currentUser.UserId &&
                           participant.Role == CourseRole.Owner, cancellationToken);

        if (!isOwner)
            throw new ForbiddenAccessException("Only course owner can perform this action.");

        var course = await dbContext.Courses.FindAsync(request.Id, cancellationToken);

        if (course is null)
            throw new NotFoundException(nameof(Course), request.Id);

        dbContext.Courses.Remove(course);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
