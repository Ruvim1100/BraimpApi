using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Domain.Entities;
using Braimp.Domain.Entities.Quizzes;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.QuizQuestions.Commands.CreateQuizQuestion;

public class CreateQuizQuestionCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork,
    IBlobStorageService blobStorageService, ILogger<CreateQuizQuestionCommandHandler> logger) : IRequestHandler<CreateQuizQuestionCommand, Guid>
{
    public async Task<Guid> Handle(CreateQuizQuestionCommand request, CancellationToken cancellationToken)
    {
        using (logger.BeginScope("CreateQuizQuestionCommand: QuizId={QuizId}", request.QuizId))
        {
            logger.LogInformation("Starting CreateQuizQuestionCommand handlingQuizId={QuizId}", request.QuizId);

            var question = new QuizQuestion
            {
                Id = Guid.NewGuid(),
                QuizId = request.QuizId,
                Text = request.Text,
                QuestionType = request.QuestionType,
                Weight = request.Weight
            };

            logger.LogDebug("Created QuizQuestion entity: QuestionId={QuestionId}", question.Id);
            dbContext.QuizQuestions.Add(question);

            if (request.Resource != null)
            {
                logger.LogDebug("Resource detected: OriginalFileName={OriginalFileName}", request.Resource.OriginalFileName);
                var extension = Path.GetExtension(request.Resource.OriginalFileName);
                var uniqueBlobName = $"{Guid.NewGuid()}{extension}";
                request.Resource.FileStream.Position = 0;


                logger.LogDebug("Uploading resource to blob storage: Container={ContainerName}, BlobName={BlobName}",
                    BlobContainers.Quizzes,
                    uniqueBlobName);

                var blobUri = await blobStorageService.UploadAsync(
                    request.Resource.FileStream,
                    containerName: BlobContainers.Quizzes,
                    blobName: uniqueBlobName,
                    encoding: request.Resource.Encoding,
                    cancellationToken);
                request.Resource.FileStream.Dispose();

                logger.LogDebug("Resource uploaded successfully: BlobUri={BlobUri}", blobUri);

                var resource = new Resource
                {
                    Id = Guid.NewGuid(),
                    Name = request.Resource.DisplayName,
                    Url = blobUri.ToString()
                };

                logger.LogDebug("Created Resource entity: ResourceId={ResourceId}, Name={ResourceName}, Url={ResourceUrl}",
                    resource.Id,
                    resource.Name,
                    resource.Url);

                dbContext.Resources.Add(resource);

                var quizQuestionFile = new QuizQuestionFile
                {
                    Id = Guid.NewGuid(),
                    ResourceId = resource.Id,
                    QuizQuestionId = question.Id

                };

                logger.LogDebug(
                    "Created QuizQuestionFile relationship: QuizQuestionFileId={QuizQuestionFileId}, QuizQuestionId={QuestionId}, ResourceId={ResourceId}",
                    quizQuestionFile.Id,
                    question.Id,
                    resource.Id);
                dbContext.QuizQuestionFiles.Add(quizQuestionFile);
            }

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
