using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Lessons.Queries.GetLessonList;
public class GetLessonListQueryValidator : AbstractValidator<GetLessonListQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetLessonListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.ModuleId)
            .NotEmpty()
            .WithMessage("ModuleId Field must be initialized");

        RuleFor(query => query)
            .MustAsync(ModuleExists)
            .WithMessage("Module doesn't exist");
    }

    private Task<bool> ModuleExists(GetLessonListQuery query, CancellationToken cancellationToken) =>
        _dbContext.Modules.AnyAsync(module => module.Id == query.ModuleId 
        && module.CourseId == query.CourseId);
}
