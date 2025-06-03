using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.DeleteCourse;
public class DeleteCourseCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteCourseCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await dbContext.Courses
            .FirstAsync(course => course.Id == request.Id, cancellationToken);

        dbContext.Courses.Remove(course);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
