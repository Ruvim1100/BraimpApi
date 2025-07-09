using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Application.Pagination;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
public class GetCourseListQueryHandler(IBraimpDbContext dbContext,
    ILogger<GetCourseListQueryHandler> logger, IBlobStorageService blobStorageService) 
    : IRequestHandler<GetCourseListQuery, PaginationResult<CourseLookupModel>>
{
    public async Task<PaginationResult<CourseLookupModel>> Handle(GetCourseListQuery request,  CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope(
                "GetCourseListQuery: Page={Page}, PageSize={PageSize}, SearchTerm={SearchTerm}, Category={Category}",
                request.Page, request.PageSize, request.SearchTerm, request.Category);

        logger.LogInformation("Starting GetCourseListQuery handling.");

        var baseQuery = dbContext.Courses
            .Where(course => !course.IsDeleted)
            .Where(course => course.Status == CourseStatus.Approved)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var pattern = $"%{request.SearchTerm}%";
            baseQuery = baseQuery.Where(course =>
                EF.Functions.Like(course.Title, pattern) ||
                (course.Description != null && EF.Functions.Like(course.Description, pattern)) ||
                course.Tags.Any(courseTag => courseTag.Tag.Name.Contains(request.SearchTerm)));
            logger.LogDebug("Applied search filter with term='{SearchTerm}'.", request.SearchTerm);
        }


        if (request.Category.HasValue)
        {
            baseQuery = baseQuery.Where(course => course.CourseCategoryId == request.Category.Value);
            logger.LogDebug("Applied category filter with Id={Category}.", request.Category);
        }

        baseQuery = (request.SortBy?.ToLower(), request.Descending) switch
        {
            ("title", true) => baseQuery.OrderByDescending(course => course.Title),
            ("title", false) => baseQuery.OrderBy(course => course.Title),
            ("createdat", true) => baseQuery.OrderByDescending(course => course.CreatedAt),
            ("createdat", false) => baseQuery.OrderBy(course => course.CreatedAt),
            _ => baseQuery.OrderByDescending(course => course.CreatedAt)
        };
        logger.LogDebug(
            "Applied sorting SortBy='{SortBy}', Descending={Descending}.",
            request.SortBy ?? "createdAt", request.Descending);

        var totalCount = await baseQuery.CountAsync(cancellationToken);
        logger.LogInformation("Total courses after filtering: {TotalCount}.", totalCount);

        var pageData = await baseQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(course => new
            {
                course.Id,
                course.Title,
                course.Description,
                course.CreatedAt,
                course.ThumbnailResourceId
            })
            .ToListAsync(cancellationToken);
        logger.LogInformation("Loaded {Count} courses for current page.", pageData.Count);

        var thumbIds = pageData
            .Where(id => id.ThumbnailResourceId.HasValue)
            .Select(id => id.ThumbnailResourceId!.Value)
            .Distinct()
            .ToList();

        var resourceInfo = await dbContext.Resources
            .Where(resource => thumbIds.Contains(resource.Id))
            .Select(resource => new { resource.Id, resource.Url, resource.Name })
            .ToDictionaryAsync(resource => resource.Id, resource => (resource.Url, resource.Name), cancellationToken);
        logger.LogDebug("Loaded metadata for {Count} resources.", resourceInfo.Count);

        TimeSpan expiry = TimeSpan.FromMinutes(5);
        string? GeneratePreviewUrl(Guid? resourceId)
        {
            if (!resourceId.HasValue || !resourceInfo.TryGetValue(resourceId.Value, out var info))
                return null;

            var token = blobStorageService
                .GetDownloadTokens(BlobContainers.Courses, info.Url, info.Name, expiry)
                .PreviewToken;
            logger.LogTrace("Generated preview token for ResourceId={ResourceId}, blobName={BlobName}.", resourceId, info.Url);
            return token;
        }

        var resultItems = pageData
            .Select(course => new CourseLookupModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreatedAt = course.CreatedAt,
                ThumbnailImageUrl = GeneratePreviewUrl(course.ThumbnailResourceId)
            })
            .ToList();

        logger.LogInformation("Finished processing. Returning {Count} items.", resultItems.Count);
        return new CourseListResponse(resultItems, request.Page, request.PageSize, totalCount);
    }
}
