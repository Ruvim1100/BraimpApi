using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Quizzes.Queries.GetQuizDetails;
public class GetQuizDetailsQueryValidator : AbstractValidator<GetQuizDetailsQuery>
{
    private readonly IBraimpDbContext _dbContext;
    public GetQuizDetailsQueryValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty");

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("CourseId cannot be empty");

        RuleFor(command => command)
            .MustAsync(QuizExists)
            .WithMessage("Quiz doesn't Exist");
    }
    private async Task<bool> QuizExists(GetQuizDetailsQuery query, CancellationToken cancellationToken) =>
        await _dbContext.Quizzes.AnyAsync(quiz => quiz.Id == query.Id &&
        quiz.CourseId == query.CourseId, cancellationToken);
}
