using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using Braimp.Application.Extensions;
using Braimp.Application.Pagination;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
public class GetCourseListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) 
    : IRequestHandler<GetCourseListQuery, PaginationResult<CourseListResponse.Item>>
{
    public async Task<PaginationResult<CourseListResponse.Item>> Handle(GetCourseListQuery request,  CancellationToken cancellationToken)
    {
        var query = dbContext.Courses
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

        if (!string.IsNullOrWhiteSpace(request.Status) &&
            Enum.TryParse<CourseStatus>(request.Status, true, out var status))
        {
            query = query.Where(c => c.Status == status);
        }

        query = (request.SortBy?.ToLower(), request.Descending) switch
        {
            ("title", true) => query.OrderByDescending(c => c.Title),
            ("title", false) => query.OrderBy(c => c.Title),
            ("createdat", true) => query.OrderByDescending(c => c.CreatedAt),
            ("createdat", false) => query.OrderBy(c => c.CreatedAt),
            _ => query.OrderByDescending(c => c.CreatedAt)
        };

        var result = await query
            .ProjectTo<CourseListResponse.Item>(mapper.ConfigurationProvider)
            .ToPagedListAsync(request, cancellationToken);

        return result;
    }
}
