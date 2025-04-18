using Braimp.Application.Abstraction;
using Braimp.Application.Common.Exceptions;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.CreateCourse;
public class CreateCourseCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateCourseCommand, Guid>
{
    public async Task<Guid> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await dbContext.CourseCategories
            .AnyAsync(course => course.Id == request.CourseCategoryId, cancellationToken);

        if (!categoryExists)
        { 
            throw new NotFoundException(nameof(CourseCategory), request.CourseCategoryId);
        }

        var course = new Course
        {
            Id = Guid.NewGuid(),
            OwnerId = request.OwnerId,
            Title = request.Title,
            Description = request.Description,
            Status = CourseStatus.Pending,
            GradingSystem = request.GradingSystem,
            CourseCategoryId = request.CourseCategoryId 
        };

        dbContext.Courses.Add(course);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return course.Id;
    }
}
