using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Quizzes.Queries.GetPublishedQuizzes;
public class GetPublishedQuizListQueryValidator : AbstractValidator<GetPublishedQuizListQuery>
{
    private readonly IBraimpDbContext _dbContext;

    public GetPublishedQuizListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.CourseId)
            .NotEmpty().WithMessage("CourseId is Required");

        RuleFor(query => query)
            .MustAsync(CourseExists);
    }

    private async Task<bool> CourseExists(GetPublishedQuizListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == query.CourseId, cancellationToken);
}
