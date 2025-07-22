using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.QuizAttempts.Queries.GetQuizAttemptList;
public class GetQuizAttemptListQueryValidator : AbstractValidator<GetQuizAttemptListQuery>
{
    private readonly IBraimpDbContext _dbContext;

    public GetQuizAttemptListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.CourseId)
            .NotEmpty().WithMessage("Course Id is Required");

        RuleFor(query => query.QuizId)
            .NotEmpty().WithMessage("QuizId is Required");

        RuleFor(query => query)
            .MustAsync(QuizExists).WithMessage("Quiz not found for given CourseId");
    }

    private async Task<bool> QuizExists(GetQuizAttemptListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Quizzes.AnyAsync(quiz => quiz.Id == query.QuizId &&
        quiz.CourseId == query.CourseId,
            cancellationToken);
}
