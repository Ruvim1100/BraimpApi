using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.LessonBlocks.Queries.GetLessonBlockList;
public class GetLessonBlockListQueryValidator : AbstractValidator<GetLessonBlockListQuery>
{
    private readonly IBraimpDbContext _dbContext;

    public GetLessonBlockListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.LessonId)
            .NotEmpty().WithMessage("LessonId is required");

        RuleFor(query => query)
            .MustAsync(LessonExists);
    }

    private async Task<bool> LessonExists(GetLessonBlockListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Lessons.AnyAsync(lesson => lesson.Id == query.LessonId, cancellationToken);
}
