using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Quizzes.Commands.DeleteQuiz;
public class DeleteQuizCommandValidator : AbstractValidator<DeleteQuizCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public DeleteQuizCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Id is required");

        RuleFor(command => command.CourseId)
            .NotEmpty()
            .WithMessage("CourseId is required");

        RuleFor(command => command)
            .MustAsync(QuizExists)
            .WithMessage("Quiz doesn't Exist");
    }

    private async Task<bool> QuizExists(DeleteQuizCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Quizzes.AnyAsync(quiz => quiz.Id == command.Id &&
        quiz.CourseId == command.CourseId, cancellationToken);
}
