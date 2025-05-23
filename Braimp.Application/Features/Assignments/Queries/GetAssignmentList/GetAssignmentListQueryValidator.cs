using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Assignments.Queries.GetAssignmentList;
public class GetAssignmentListQueryValidator : AbstractValidator<GetAssignmentListQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetAssignmentListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.CourseId)
            .NotEmpty().WithMessage("CourseId is required")
            .NotEqual(Guid.Empty).WithMessage("CourseId cannot be null");

        RuleFor(query => query)
            .MustAsync(CourseExists);
    }

    private async Task<bool> CourseExists(GetAssignmentListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == query.CourseId, cancellationToken);
}
