using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Queries.GetCourseDetails;
public class GetCourseDetailsQueryValidator : AbstractValidator<GetCourseDetailQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetCourseDetailsQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.Id)
            .NotEmpty()
            .WithMessage("Course Id is requred");

        RuleFor(query => query)
            .MustAsync(CourseExists);
    }

    private async Task<bool> CourseExists(GetCourseDetailQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == query.Id, cancellationToken);
}
