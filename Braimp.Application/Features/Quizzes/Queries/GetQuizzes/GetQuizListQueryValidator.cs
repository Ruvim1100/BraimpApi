using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Quizzes.Queries.GetQuizzes;
public class GetQuizListQueryValidator : AbstractValidator<GetQuizListQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetQuizListQueryValidator(IBraimpDbContext dbContext) 
    {
        _dbContext = dbContext;

        RuleFor(query => query.CourseId)
            .NotEmpty()
            .WithMessage("CourseId cannot be empty");

        RuleFor(query => query)
            .MustAsync(CourseExists)
            .WithMessage("Course doesn't exist");
    }

    private async Task<bool> CourseExists(GetQuizListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Courses.AnyAsync(course => course.Id == query.CourseId, cancellationToken);
}
