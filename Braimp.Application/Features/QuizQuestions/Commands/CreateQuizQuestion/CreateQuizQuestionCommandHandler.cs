using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Domain.Entities;
using Braimp.Domain.Entities.Quizzes;
using MediatR;

namespace Braimp.Application.Features.QuizQuestions.Commands.CreateQuizQuestion;

public class CreateQuizQuestionCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork,
    IBlobStorageService blobStorageService) : IRequestHandler<CreateQuizQuestionCommand, Guid>
{
    public async Task<Guid> Handle(CreateQuizQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = new QuizQuestion
        {
            Id = Guid.NewGuid(),
            QuizId = request.QuizId,
            Text = request.Text,
            QuestionType = request.QuestionType,
            Weight = request.Weight
        };

        dbContext.QuizQuestions.Add(question);

        if (request.Resource != null)
        {
            var extension = Path.GetExtension(request.Resource.OriginalFileName);
            var uniqueBlobName = $"{Guid.NewGuid()}{extension}";
            request.Resource.FileStream.Position = 0;


            var blobUri = await blobStorageService.UploadAsync(
                request.Resource.FileStream,
                containerName: BlobContainers.Quizzes,
                blobName: uniqueBlobName,
                encoding: request.Resource.Encoding,
                cancellationToken);
            request.Resource.FileStream.Dispose();

            var resource = new Resource
            {
                Id = Guid.NewGuid(),
                Name = request.Resource.DisplayName,
                Url = blobUri.ToString()
            };

            dbContext.Resources.Add(resource);

            var quizQuestionFile = new QuizQuestionFile
            {
                Id = Guid.NewGuid(),
                ResourceId = resource.Id,
                QuizQuestionId = question.Id

            };

            dbContext.QuizQuestionFiles.Add(quizQuestionFile);
        }

        if (request.QuizOptions != null)
        {
            var options = request.QuizOptions.Select(option => new QuizOption
            {
                Id = Guid.NewGuid(),
                Text = option.Text,
                IsCorrect = option.IsCorrect,
                QuizQuestionId = question.Id
            }).ToList();

            dbContext.QuizOptions.AddRange(options);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false); ;
        return question.Id;
    }
}
