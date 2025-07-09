using AutoMapper;
using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Lessons.Queries.GetLessonDetails;
public class GetLessonDetailsQueryHandler(IBraimpDbContext dbContext, IMapper mapper, IBlobStorageService blobStorageService)
    : IRequestHandler<GetLessonDetailsQuery, LessonDetailsResponse>
{
    public async Task<LessonDetailsResponse> Handle(GetLessonDetailsQuery request, CancellationToken cancellationToken)
    {
        var lesson = await dbContext.Lessons
            .Include(lesson => lesson.LessonFiles)
            .FirstAsync(lesson => lesson.Id == request.Id, cancellationToken);

        var resourceIds = lesson.LessonFiles.Select(file => file.ResourceId).ToList();

        var resources = await dbContext.Resources
            .Where(resource => resourceIds.Contains(resource.Id))
            .ToListAsync(cancellationToken);
            
        var response = mapper.Map<LessonDetailsResponse>(lesson);

        foreach (var lessonFile in lesson.LessonFiles)
        {
            var resource = resources.FirstOrDefault(resource => resource.Id == lessonFile.ResourceId);
            if (resource is null) continue; 

            var resourceUrl = blobStorageService
                .GetDownloadTokens(BlobContainers.Lessons, resource.Url, resource.Name, TimeSpan.FromMinutes(5))
                .DownloadToken;

            response.Files.Add(new FileResourceModel
            {
                Id = lessonFile.Id,
                DownloadUrl = resourceUrl,
                Name = resource.Name
            });
        }


        return response;
    }
}
