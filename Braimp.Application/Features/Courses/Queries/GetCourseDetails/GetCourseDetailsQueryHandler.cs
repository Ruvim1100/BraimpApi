using AutoMapper;
using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.Courses.Queries.GetCourseDetails;
public class GetCourseDetailsQueryHandler(IBraimpDbContext dbContext, IMapper mapper, ILogger<GetCourseDetailsQueryHandler> logger,
    ICurrentUserService currentUser, IBlobStorageService blobStorageService) 
    : IRequestHandler<GetCourseDetailQuery, CourseDetailsResponse>
{

    public async Task<CourseDetailsResponse> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation(
                    "Starting GetCourseDetailQuery handling: CourseId={CourseId}, UserId={UserId}",
                    request.Id,
                    currentUser.UserId);


        var course = await dbContext.Courses
            .Where(course => !course.IsDeleted)
            .Include(course => course.CourseCategory)
            .Include(course => course.Tags)
                .ThenInclude(courseTag => courseTag.Tag)
            .FirstAsync(item => item.Id == request.Id, cancellationToken);


        string? bannerToken = null;
        string? thumbnailToken = null;

        if (course.BannerResourceId.HasValue || course.ThumbnailResourceId.HasValue)
        {
            var resourceIds = new[] { course.BannerResourceId, course.ThumbnailResourceId }
                .Where(id => id.HasValue)
                .Select(id => id!.Value)
                .ToList();

            var resources = await dbContext.Resources
                .Where(resource => resourceIds.Contains(resource.Id))
                .ToListAsync(cancellationToken);

            string? GetToken(Guid? resourceId)
            {
                var resource = resources.FirstOrDefault(r => r.Id == resourceId);
                if (resource is null) return null;

                return blobStorageService
                    .GetDownloadTokens(BlobContainers.Courses, resource.Url, resource.Name, TimeSpan.FromMinutes(5))
                    .PreviewToken;
            }

            bannerToken = GetToken(course.BannerResourceId);
            thumbnailToken = GetToken(course.ThumbnailResourceId);
        }

        logger.LogInformation(
            "GetCourseDetailQuery completed successfully: CourseId={CourseId}",
            request.Id);

        var response = mapper.Map<CourseDetailsResponse>(course);

        response.ThumbnailImageUrl = thumbnailToken;
        response.BannerImageUrl = bannerToken;

        return response;
    }
}
