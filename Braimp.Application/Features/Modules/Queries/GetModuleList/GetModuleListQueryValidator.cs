using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Queries.GetModuleList;
public class GetModuleListQueryValidator : AbstractValidator<GetModuleListQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetModuleListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.CourseId)
            .NotEmpty()
            .WithMessage("CourseId must be provided.");

        RuleFor(query => query)
            .MustAsync(CourseExists).WithMessage("Course doesn't exist");
    }

    private async Task<bool> CourseExists(GetModuleListQuery query, CancellationToken cancellationToken) => 
        await _dbContext.Courses.AnyAsync(course => course.Id == query.CourseId, cancellationToken);
}