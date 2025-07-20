using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using Braimp.Application.Pagination;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Queries.GetEnrolledCourseList;
public class GetEnrolledCourseListQueryHandler(IBraimpDbContext dbContext, IBlobStorageService blobStorageService,
    ICurrentUserService currentUserService) : IRequestHandler<GetEnrolledCourseListQuery, PaginationResult<EnrollmentRequestLookupModel>>
{
    public async Task<PaginationResult<EnrollmentRequestLookupModel>> Handle(GetEnrolledCourseListQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = dbContext.Courses
            .Where(course => course.Status == CourseStatus.Approved)
            .Where(course => !course.IsDeleted)
            .Where(course => course.Participants
                .Any(participant => participant.UserId == currentUserService.UserId
                       && participant.Role == CourseRole.Student))
            .OrderBy(course => course.CreatedAt)
            .AsNoTracking();

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var pageData = await baseQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(course => new
            {
                course.Id,
                course.Title,
                course.Description,
                course.ThumbnailResourceId
            })
            .ToListAsync(cancellationToken);

        var thumbIds = pageData
            .Where(id => id.ThumbnailResourceId.HasValue)
            .Select(id => id.ThumbnailResourceId!.Value)
            .Distinct()
            .ToList();

        var resourceMap = await dbContext.Resources
            .Where(resource => thumbIds.Contains(resource.Id))
            .Select(resource => new { resource.Id, resource.Url, resource.Name })
            .ToDictionaryAsync(resource => resource.Id, resource => (resource.Url, resource.Name), cancellationToken);

        TimeSpan expiry = TimeSpan.FromMinutes(5);
        string? GeneratePreviewUrl(Guid? resourceId)
        {
            if (!resourceId.HasValue || !resourceMap.TryGetValue(resourceId.Value, out var info))
                return null;

            var token = blobStorageService
                .GetDownloadTokens(BlobContainers.Courses, info.Url, info.Name, expiry)
                .PreviewToken;
            return token;
        }


        var resultItems = pageData
            .Select(course => new EnrollmentRequestLookupModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                ThumbnailImageUrl = GeneratePreviewUrl(course.ThumbnailResourceId)
            })
            .ToList();

        return new EnrolledCourseListResponse(
            resultItems,
            request.Page,
            request.PageSize,
            totalCount);
    }
}
