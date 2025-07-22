using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Quizzes;
using Braimp.Domain.Entities.Quizzes.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.QuizAttempts.Commands.SubmitQuizAnswers;

public class SubmitQuizAnswersCommandHandler(
    IBraimpDbContext dbContext,
    IUnitOfWork unitOfWork
) : IRequestHandler<SubmitQuizAnswersCommand, Unit>
{
    public async Task<Unit> Handle(
        SubmitQuizAnswersCommand request,
        CancellationToken cancellationToken)
    {
        if (request.Answers == null || !request.Answers.Any())
            throw new InvalidOperationException("No answers provided.");

        var quizAttempt = await dbContext.QuizAttempts
            .Include(x => x.Quiz)
                .ThenInclude(q => q.Questions)
                    .ThenInclude(qn => qn.QuestionOptions)
            .FirstOrDefaultAsync(x => x.Id == request.QuizAttemptId, cancellationToken);

        if (quizAttempt is null)
            throw new InvalidOperationException(
                $"QuizAttempt with ID {request.QuizAttemptId} not found.");

        var now = DateTimeOffset.UtcNow;
        var attemptAnswers = new List<AttemptAnswer>();
        int correctCount = 0, incorrectCount = 0;
        int weightedScore = 0;

        foreach (var ua in request.Answers.DistinctBy(a => a.QuestionId))
        {
            var question = quizAttempt.Quiz.Questions
                .FirstOrDefault(q => q.Id == ua.QuestionId)
                ?? throw new InvalidOperationException(
                    $"Question with ID {ua.QuestionId} not found in quiz.");

            var aa = new AttemptAnswer
            {
                Id = Guid.NewGuid(),
                QuizAttemptId = quizAttempt.Id,
                QuestionText = question.Text,
                QuestionType = question.QuestionType,
                Weight = question.Weight,
                OriginalQuestionId = question.Id
            };

            bool isCorrect = false;
            switch (question.QuestionType)
            {
                case QuestionType.SingleChoice:
                    if (ua.SelectedOptionIds?.Count != 1)
                        throw new InvalidOperationException(
                            "SingleChoice answer must contain exactly one selectedOptionId.");

                    var sel = question.QuestionOptions
                        .FirstOrDefault(o => o.Id == ua.SelectedOptionIds[0])
                        ?? throw new InvalidOperationException(
                            $"Option with ID {ua.SelectedOptionIds[0]} not found.");

                    aa.AnswerOptions.Add(new AnswerOption
                    {
                        Id = Guid.NewGuid(),
                        AttemptAnswerId = aa.Id,
                        Text = sel.Text,
                        IsCorrect = sel.IsCorrect,
                        IsSelected = true,
                        OriginalOptionId = sel.Id
                    });

                    isCorrect = sel.IsCorrect;
                    break;

                case QuestionType.MultipleChoice:
                    if (ua.SelectedOptionIds == null || ua.SelectedOptionIds.Count == 0)
                        throw new InvalidOperationException(
                            "MultipleChoice answer must contain at least one selectedOptionId.");

                    var selectedIds = ua.SelectedOptionIds.ToHashSet();
                    var correctIds = question.QuestionOptions
                        .Where(o => o.IsCorrect)
                        .Select(o => o.Id)
                        .ToHashSet();

                    isCorrect = selectedIds.SetEquals(correctIds);
                    foreach (var oid in selectedIds)
                    {
                        var opt = question.QuestionOptions
                            .FirstOrDefault(o => o.Id == oid)
                            ?? throw new InvalidOperationException(
                                $"Option with ID {oid} not found.");

                        aa.AnswerOptions.Add(new AnswerOption
                        {
                            Id = Guid.NewGuid(),
                            AttemptAnswerId = aa.Id,
                            Text = opt.Text,
                            IsCorrect = opt.IsCorrect,
                            IsSelected = true,
                            OriginalOptionId = opt.Id
                        });
                    }
                    break;

                case QuestionType.Text:
                    if (string.IsNullOrWhiteSpace(ua.TextAnswer))
                        throw new InvalidOperationException(
                            $"Text answer cannot be empty for question {question.Id}.");

                    aa.AnswerTexts.Add(new AnswerText
                    {
                        Id = Guid.NewGuid(),
                        AttemptAnswerId = aa.Id,
                        Text = ua.TextAnswer
                    });
                    break;

                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(question.QuestionType),
                        $"Unsupported question type: {question.QuestionType}");
            }

            if (isCorrect)
            {
                correctCount++;
                weightedScore += question.Weight;
            }
            else if (question.QuestionType != QuestionType.Text)
            {
                incorrectCount++;
            }

            attemptAnswers.Add(aa);
        }

        quizAttempt.FinishedAt = now;
        quizAttempt.CorrectAnswerCount = correctCount;
        quizAttempt.IncorrectAnswerCount = incorrectCount;
        quizAttempt.Score = weightedScore;     
        quizAttempt.IsPublished = true;

        dbContext.AttemptAnswers.AddRange(attemptAnswers);
        dbContext.QuizAttempts.Update(quizAttempt);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
