using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Lessons.Queries.GetPublishedLessonList;
public class GetPublishedLessonListQueryValidator : AbstractValidator<GetPublishedLessonListQuery>
{
    private readonly IBraimpDbContext _dbContext;

    public GetPublishedLessonListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.CourseId)
            .NotEmpty().WithMessage("CourseId is required");

        RuleFor(query => query.ModuleId)
            .NotEmpty().WithMessage("ModuleId is required");

        RuleFor(query => query)
            .MustAsync(ModuleExists).WithMessage("Module doesn't exist");
    }

    private async Task<bool> ModuleExists(GetPublishedLessonListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Modules.AnyAsync(module => module.Id == query.ModuleId && 
        module.CourseId == query.CourseId, cancellationToken);
}
