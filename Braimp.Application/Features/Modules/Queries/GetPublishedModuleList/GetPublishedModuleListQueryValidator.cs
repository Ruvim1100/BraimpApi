using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Queries.GetPublishedModuleList;
public class GetPublishedModuleListQueryValidator : AbstractValidator<GetPublishedModuleListQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetPublishedModuleListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.CourseId)
            .NotEmpty()
            .WithMessage("CourseId must be provided.");

        RuleFor(query => query)
            .MustAsync(CourseExists).WithMessage("Course doesn't exist");
    }

    private async Task<bool> CourseExists(GetPublishedModuleListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == query.CourseId, cancellationToken);
}
