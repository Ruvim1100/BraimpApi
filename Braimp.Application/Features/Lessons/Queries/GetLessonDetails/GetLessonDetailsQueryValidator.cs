using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Lessons.Queries.GetLessonDetails;
public class GetLessonDetailsQueryValidator : AbstractValidator<GetLessonDetailsQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetLessonDetailsQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.Id)
            .NotEmpty()
            .WithMessage("Id field must be initialized");

        RuleFor(query => query)
            .MustAsync(LessonExists)
            .WithMessage("Lesson doesn't exist");
    }

    public Task<bool> LessonExists(GetLessonDetailsQuery query, CancellationToken cancellationToken) =>
        _dbContext.Lessons.AnyAsync(lesson => lesson.Id == query.Id
            && lesson.ModuleId == query.ModuleId
            && lesson.Module.CourseId == query.CourseId, cancellationToken);
}
