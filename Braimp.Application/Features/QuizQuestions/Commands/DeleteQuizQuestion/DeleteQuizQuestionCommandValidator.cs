using Braimp.Application.Abstraction;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.QuizQuestions.Commands.DeleteQuizQuestion;
public class DeleteQuizQuestionCommandValidator : AbstractValidator<DeleteQuizQuestionCommand>
{
    private readonly IBraimpDbContext _dbContext;

    public DeleteQuizQuestionCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(command => command.Id)
            .NotEmpty().WithMessage("Id is Required");

        RuleFor(command => command.QuizId)
            .NotEmpty().WithMessage("QuizId is Required");

        RuleFor(command => command.CourseId)
            .NotEmpty().WithMessage("CourseId is Required");

        RuleFor(command => command)
            .MustAsync(QuestionExists).WithMessage("The specified question does not exist or does not belong to the provided quiz and course.");

    }

    private async Task<bool> QuestionExists(DeleteQuizQuestionCommand command, CancellationToken cancellationToken) =>
        await _dbContext.QuizQuestions
        .AnyAsync(question => question.Id == command.Id &&
        question.QuizId == command.QuizId &&
        question.Quiz.CourseId == command.CourseId,
            cancellationToken);
}
