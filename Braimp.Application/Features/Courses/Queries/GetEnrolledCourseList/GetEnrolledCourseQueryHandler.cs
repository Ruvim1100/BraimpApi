using AutoMapper;
using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Queries.GetEnrolledCourseList;
public class GetEnrolledCourseQueryHandler(IBraimpDbContext dbContext, IMapper mapper, IBlobStorageService blobStorageService,
    ICurrentUserService currentUser) : IRequestHandler<GetEnrolledCourseListQuery, EnrolledCourseListResponse>
{
    public async Task<EnrolledCourseListResponse> Handle(GetEnrolledCourseListQuery request, CancellationToken cancellationToken)
    {
        var courses = await dbContext.Courses
            .Where(course => course.Participants
                .Any(p => p.UserId == currentUser.UserId && p.Role == CourseRole.Student))
            .Include(c => c.Image)
            .ToListAsync(cancellationToken);

        var imageResourceIds = courses
            .Where(course => course.Image != null)
            .Select(course => course.Image!.ResourceId)
            .Distinct()
            .ToList();

        var resources = await dbContext.Resources
            .Where(resource => imageResourceIds.Contains(resource.Id))
            .ToDictionaryAsync(resource => resource.Id, cancellationToken);

        var courseLookupList = courses.Select(course =>
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

        return new EnrolledCourseListResponse { Courses = courseLookupList };
    }
}
