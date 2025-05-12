using Braimp.Application.Abstraction;
using Braimp.Application.Exceptions;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.UpdateCourse;
public class UpdateCourseCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    ICurrentUserService currentUser, ICourseAuthorizationService courseAuthorizationService) 
    : IRequestHandler<UpdateCourseCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        await courseAuthorizationService.EnsureUserHasRole(request.Id, currentUser.UserId, 
            CourseRole.Owner, CourseRole.Assistant);

        var course = await dbContext.Courses.FindAsync(request.Id, cancellationToken); ;

        if (course is null)
            throw new NotFoundException(nameof(Course), request.Id);

        if (request.CourseCategoryId.HasValue)
        {
            var exists = await dbContext.CourseCategories
                .AnyAsync(cat => cat.Id == request.CourseCategoryId.Value, cancellationToken);

            if (!exists)
                throw new NotFoundException(nameof(CourseCategory), request.CourseCategoryId);

            course.CourseCategoryId = request.CourseCategoryId.Value;
        }

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
