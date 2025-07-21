using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Quizzes.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.QuizQuestions.Commands.CreateQuizQuestion;
public class CreateQuizQuestionCommandValidator : AbstractValidator<CreateQuizQuestionCommand>
{
    private readonly IBraimpDbContext _dbContext;
    public CreateQuizQuestionCommandValidator(IBraimpDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(cmd => cmd.QuizId)
            .NotEmpty().
            WithMessage("QuizId cannot be empty");

        RuleFor(cmd => cmd.CourseId)
            .NotEmpty()
            .WithMessage("CourseId cannot be empty");

        RuleFor(command => command.Text)
            .NotEmpty()
            .WithMessage("Text is required.");

        RuleFor(command => command.Weight)
            .GreaterThan(0)
            .WithMessage("Weight must be greater than zero.");

        RuleFor(command => command)
            .MustAsync(QuizExists)
            .WithMessage("Quiz does not exist.");

        RuleFor(cmd => cmd.QuestionType)
          .IsInEnum()
          .WithMessage("Invalid question type.");

        When(command => command.QuestionType == QuestionType.SingleChoice, () =>
        {
            RuleFor(command => command.QuizOptions)
                .NotNull().WithMessage("Options are required for choice questions.")
                .Must(option => option!.Count >= 2).WithMessage("At least two options are required.");

            RuleFor(command => command.QuizOptions)
                .Must(option => option!.Count(option => option.IsCorrect) == 1)
                .WithMessage("Exactly one correct option must be selected for single-choice questions.");
        });

        When(command => command.QuestionType == QuestionType.MultipleChoice, () =>
        {
            RuleFor(command => command.QuizOptions)
                .NotNull().WithMessage("Options are required for choice questions.")
                .Must(option => option!.Count >= 2).WithMessage("At least two options are required.");

            RuleFor(command => command.QuizOptions)
                .Must(option => option!.Count(option => option.IsCorrect) >= 1)
                .WithMessage("At least one correct option must be selected for multiple-choice questions.");
        });

        When(command => command.QuestionType == QuestionType.Text, () =>
        {
            RuleFor(command => command.QuizOptions)
                .Empty()
                .WithMessage("Options must be empty for text questions.");
        });

    }

    private async Task<bool> QuizExists(CreateQuizQuestionCommand command, CancellationToken cancellationToken) =>
        await _dbContext.Quizzes
        .AnyAsync(quiz => quiz.Id == command.QuizId && 
        quiz.CourseId == command.CourseId, cancellationToken);
}
