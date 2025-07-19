using AutoMapper;
using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.News.Queries.GetCourseNewsList;
public class GetCourseNewsListQueryHandler(IBraimpDbContext dbContext, IMapper mapper, 
    IBlobStorageService blobStorageService) : IRequestHandler<GetCourseNewsListQuery, CourseNewsListResponse>
{
    public async Task<CourseNewsListResponse> Handle(GetCourseNewsListQuery request, CancellationToken cancellationToken)
    {
        var news = await dbContext.CourseNews
            .Where(news => news.CourseId == request.CourseId)
            .ToListAsync(cancellationToken);

        var resourceIds = news
            .Select(news => news.ImageResourceId)
            .Distinct()
            .ToList();

        var resources = await dbContext.Resources
            .Where(resource => resourceIds.Contains(resource.Id))
            .ToDictionaryAsync(resource => resource.Id, cancellationToken);

        var newsList = new List<CourseNewsLookupModel>();

        foreach (var newsItem in news)
        {
            var responseItem = mapper.Map<CourseNewsLookupModel>(newsItem);

            if (resources.TryGetValue(newsItem.ImageResourceId, out var resource))
            {
                var resourceUrl = blobStorageService
                    .GetDownloadTokens(BlobContainers.News, resource.Url, resource.Name, TimeSpan.FromMinutes(5))
                    .PreviewToken;

                responseItem.ImageUrl = resourceUrl;
            }

            newsList.Add(responseItem);
        }

        return new CourseNewsListResponse { CourseNews = newsList };
    }
}
