using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Quizzes;
using Braimp.Domain.Entities.Quizzes.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.QuizAttempts.Commands.SubmitQuizAnswers;
public class SubmitQuizAnswersCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork)
    : IRequestHandler<SubmitQuizAnswersCommand, Unit>
{
    public async Task<Unit> Handle(SubmitQuizAnswersCommand request, CancellationToken cancellationToken)
    {
        var quizAttempt = await dbContext.QuizAttempts
                    .Include(quizAttempt => quizAttempt.Quiz)
                    .ThenInclude(quiz => quiz.Questions)
                    .ThenInclude(question => question.QuestionOptions)
                    .FirstAsync(x => x.Id == request.QuizAttemptId, cancellationToken);


        var attemptAnswers = new List<AttemptAnswer>();

        foreach (var userAnswer in request.Answers)
        {
            var question = quizAttempt.Quiz.Questions.FirstOrDefault(q => q.Id == userAnswer.QuestionId);
            if (question is null)
                continue;

            var attemptAnswer = new AttemptAnswer
            {
                Id = Guid.NewGuid(),
                QuizAttemptId = quizAttempt.Id,
                QuestionText = question.Text,
                QuestionType = question.QuestionType,
                Weight = question.Weight,
                OriginalQuestionId = question.Id,
            };

            switch (userAnswer.Type)
            {
                case QuestionType.SingleChoice:
                    var singleOption = question.QuestionOptions.FirstOrDefault(o => o.Id == userAnswer.SelectedOptionId);
                    if (singleOption != null)
                    {
                        attemptAnswer.AnswerOptions.Add(new AnswerOption
                        {
                            Id = Guid.NewGuid(),
                            AttemptAnswerId = attemptAnswer.Id,
                            Text = singleOption.Text,
                            IsCorrect = singleOption.IsCorrect,
                            IsSelected = true,
                            OriginalOptionId = singleOption.Id
                        });
                    }
                    break;

                case QuestionType.MultipleChoice:
                    foreach (var optionId in userAnswer.SelectedOptionIds ?? [])
                    {
                        var option = question.QuestionOptions.FirstOrDefault(o => o.Id == optionId);
                        if (option != null)
                        {
                            attemptAnswer.AnswerOptions.Add(new AnswerOption
                            {
                                Id = Guid.NewGuid(),
                                AttemptAnswerId = attemptAnswer.Id,
                                Text = option.Text,
                                IsCorrect = option.IsCorrect,
                                IsSelected = true,
                                OriginalOptionId = option.Id
                            });
                        }
                    }
                    break;

                case QuestionType.Text:
                    if (!string.IsNullOrWhiteSpace(userAnswer.TextAnswer))
                    {
                        attemptAnswer.AnswerTexts.Add(new AnswerText
                        {
                            Id = Guid.NewGuid(),
                            AttemptAnswerId = attemptAnswer.Id,
                            Text = userAnswer.TextAnswer
                        });
                    }
                    break;
            }

            attemptAnswers.Add(attemptAnswer);
        }

        dbContext.AttemptAnswers.AddRange(attemptAnswers);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
