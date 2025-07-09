using AutoMapper;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.QuizQuestions.Queries.GetQuizQuestionList;
public class GetQuizQuestionListQueryHandler(IBraimpDbContext dbContext, IMapper mapper, IBlobStorageService blobStorageService) 
    : IRequestHandler<GetQuizQuestionListQuery, QuizQestionListResponse>
{
    public async Task<QuizQestionListResponse> Handle(GetQuizQuestionListQuery request, CancellationToken cancellationToken)
    {
        var questions = await dbContext.QuizQuestions
            .Where(question => question.QuizId == request.QuizId)
            .Include(question => question.QuestionOptions)
            .Include(question => question.QuizQuestionFile)
            .OrderBy(question => question.SortIndex)
            .ToListAsync(cancellationToken);

        var fileResourceIds = questions
            .Where(question => question.QuizQuestionFile != null)
            .Select(question => question.QuizQuestionFile!.ResourceId)
            .Distinct()
            .ToList();

        var rescources = await dbContext.Resources
            .Where(resource => fileResourceIds.Contains(resource.Id))
            .ToDictionaryAsync(resource => resource.Id, cancellationToken);

        var questionsLookupList = questions.Select(question =>
        {
            var model = mapper.Map<QuizQuestionLookupModel>(question);

            if (question.QuizQuestionFile != null 
            && rescources.TryGetValue(question.QuizQuestionFile.ResourceId, out var resource))
            {
                var (previewUrl, _) = blobStorageService.GetDownloadTokens(
                    containerName: "quizzes",
                    blobName: resource.Url,
                    fileName: resource.Name,
                    expiry: TimeSpan.FromMinutes(5)
                    );

                model.File = previewUrl;
            }
            return model;
        }).ToList();
        return new QuizQestionListResponse { Questions = questionsLookupList };
    }
}
