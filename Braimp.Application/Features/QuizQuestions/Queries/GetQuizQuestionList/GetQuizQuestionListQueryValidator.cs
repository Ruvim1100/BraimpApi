using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.QuizQuestions.Queries.GetQuizQuestionList;
public class GetQuizQuestionListQueryValidator : AbstractValidator<GetQuizQuestionListQuery>
{
    private readonly IBraimpDbContext _dbContext;

    public GetQuizQuestionListQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(query => query.CourseId)
            .NotEmpty().WithMessage("CourseId is required");

        RuleFor(query => query.QuizId)
            .NotEmpty().WithMessage("QuizId is Required");

        RuleFor(query => query)
            .MustAsync(QuizExists).WithMessage("Quiz with the specified ID does not exist");
    }

    private async Task<bool> QuizExists(GetQuizQuestionListQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Quizzes.
        AnyAsync(quiz => quiz.Id == query.QuizId && 
        quiz.CourseId == query.CourseId, 
            cancellationToken);
}
