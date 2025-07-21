using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Quizzes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.QuizQuestions.Commands.CreateQuizQuestion;

public class CreateQuizQuestionCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    ILogger<CreateQuizQuestionCommandHandler> logger) : IRequestHandler<CreateQuizQuestionCommand, Guid>
{
    public async Task<Guid> Handle(CreateQuizQuestionCommand request, CancellationToken cancellationToken)
    {
        using (logger.BeginScope("CreateQuizQuestionCommand: QuizId={QuizId}", request.QuizId))
        {
            logger.LogInformation("Starting CreateQuizQuestionCommand handlingQuizId={QuizId}", request.QuizId);

            var maxSortIndex = await dbContext.QuizQuestions
                .Where(question => question.QuizId == request.QuizId)
                .MaxAsync(question => (int?)question.SortIndex, cancellationToken) ?? -1;

            var question = new QuizQuestion
            {
                Id = Guid.NewGuid(),
                QuizId = request.QuizId,
                Text = request.Text,
                QuestionType = request.QuestionType,
                SortIndex = maxSortIndex + 1,
                Weight = request.Weight
            };

            logger.LogDebug("Created QuizQuestion entity: QuestionId={QuestionId}", question.Id);
            dbContext.QuizQuestions.Add(question);

            if (request.QuizOptions != null)
            {
                var options = request.QuizOptions.Select(option => new QuestionOption
                {
                    Id = Guid.NewGuid(),
                    Text = option.Text,
                    IsCorrect = option.IsCorrect,
                    QuizQuestionId = question.Id
                }).ToList();

                dbContext.QuestionOptions.AddRange(options);
            }
            else
            {
                logger.LogWarning("No resource provided for QuizQuestion.");
            }
            await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            logger.LogInformation(
                "CreateQuizQuestionCommand completed successfully: QuizQuestionId={QuestionId}",
                question.Id);

            return question.Id;
        }
    }
}
