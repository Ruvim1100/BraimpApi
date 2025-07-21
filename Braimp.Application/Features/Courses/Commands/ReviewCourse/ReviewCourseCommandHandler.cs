using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.ReviewCourse;
public class ReviewCourseCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<ReviewCourseCommand, Unit>
{
    public async Task<Unit> Handle(ReviewCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await dbContext.Courses
            .FirstAsync(course => course.Id == request.CourseId,
            cancellationToken);

        course.Status = request.Status;
        dbContext.Courses.Update(course);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
