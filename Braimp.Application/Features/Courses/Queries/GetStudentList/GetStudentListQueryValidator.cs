using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Queries.GetStudentList;
public class GetStudentListQueryValidator : AbstractValidator<GetStudentListQuery>
{
    private readonly IBraimpDbContext _dbContext;

    public GetStudentListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.CourseId)
            .NotEmpty().WithMessage("CourseId is required.");

        RuleFor(query => query)
            .MustAsync(CourseExists).WithMessage("Course does not exist."); ;
    }

    private async Task<bool> CourseExists(GetStudentListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Courses
        .AnyAsync(course => course.Id == query.CourseId, 
            cancellationToken);
}
