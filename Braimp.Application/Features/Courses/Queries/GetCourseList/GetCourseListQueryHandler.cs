using AutoMapper;
using Braimp.Application.Abstraction;
using Braimp.Application.Pagination;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
public class GetCourseListQueryHandler(IBraimpDbContext dbContext, IMapper mapper, 
    ILogger<GetCourseListQueryHandler> logger, IBlobStorageService blobStorageService) 
    : IRequestHandler<GetCourseListQuery, PaginationResult<CourseLookupModel>>
{
    public async Task<PaginationResult<CourseLookupModel>> Handle(GetCourseListQuery request,  CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Starting GetCourseListQuery handling: SearchTerm={SearchTerm}, Category={Category}, SortBy={SortBy}, Descending={Descending}",
            request.SearchTerm,
            request.Category,
            request.SortBy,
            request.Descending);

        var query = dbContext.Courses
            .Where(course => course.Status == CourseStatus.Approved)
            .Include(course => course.Image)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var pattern = $"%{request.SearchTerm}%";
            query = query.Where(course => EF.Functions.Like(course.Title, pattern) ||
                (course.Description != null && EF.Functions.Like(course.Description, pattern))
            );
        }

        if (request.Category.HasValue)
        {
            query = query.Where(c => c.CourseCategoryId == request.Category.Value);
        }

        query = (request.SortBy?.ToLower(), request.Descending) switch
        {
            ("title", true) => query.OrderByDescending(c => c.Title),
            ("title", false) => query.OrderBy(c => c.Title),
            ("createdat", true) => query.OrderByDescending(c => c.CreatedAt),
            ("createdat", false) => query.OrderBy(c => c.CreatedAt),
            _ => query.OrderByDescending(c => c.CreatedAt)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var imageResourceIds = items
            .Where(course => course.Image != null)
            .Select(course => course.Image!.ResourceId)
            .Distinct()
            .ToList();

        var resources = await dbContext.Resources
            .Where(resource => imageResourceIds.Contains(resource.Id))
            .ToDictionaryAsync(resource => resource.Id, cancellationToken);

        var courseLookupList = items.Select(course =>
        {
            var model = mapper.Map<CourseLookupModel>(course);
            if (course.Image != null && resources.TryGetValue(course.Image.ResourceId, out var resource))
            {
                var (previewUrl, _) = blobStorageService.GetDownloadTokens(
                    containerName: "courses",
                    blobName: resource.Url,
                    fileName: resource.Name,
                    expiry: TimeSpan.FromHours(1)
                );

                model.ThumbnailImage = previewUrl;
            }
            return model;

        }).ToList();

        logger.LogInformation(
            "GetCourseListQuery completed successfully: returned {Count} items, Page={Page}, PageSize={PageSize}",
            courseLookupList.Count,
            request.Page,
            request.PageSize);

        return new PaginationResult<CourseLookupModel>(
            courseLookupList,
            request.Page,
            request.PageSize,
            totalCount
        );
    }
}
