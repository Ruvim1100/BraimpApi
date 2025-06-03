using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.UpdateCourse;
public class UpdateCourseCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateCourseCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await dbContext.Courses
            .FirstAsync(course => course.Id == request.Id, cancellationToken);

        if (request.CourseCategoryId.HasValue)
            course.CourseCategoryId = request.CourseCategoryId.Value;

        if (request.Title is not null)
            course.Title = request.Title;

        if (request.Description is not null)
            course.Description = request.Description;

        if (request.CoverImageUrl is not null)
            course.CoverImageUrl = request.CoverImageUrl;

        if (request.BackgroundColor is not null)
            course.BackgroundColor = request.BackgroundColor;

        if (request.LogoUrl is not null)
            course.LogoUrl = request.LogoUrl;

        if (request.GradingSystem.HasValue)
            course.GradingSystem = request.GradingSystem.Value;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
