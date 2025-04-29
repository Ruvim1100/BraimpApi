using Braimp.Application.Common.Pagination;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
public class CourseListResponse
{

    public IList<CourseLookupDto> Courses { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
    public bool HasPreviousPage { get; init; }
    public bool HasNextPage { get; init; }

    public CourseListResponse(PagedList<CourseLookupDto> pagedList)
    {
        if (pagedList == null) throw new ArgumentNullException(nameof(pagedList));

        Courses = pagedList.Items;
        Page = pagedList.Page;
        PageSize = pagedList.PageSize;
        TotalCount = pagedList.TotalCount;
        TotalPages = pagedList.TotalPages;
        HasPreviousPage = pagedList.HasPreviousPage;
        HasNextPage = pagedList.HasNextPage;
    }
}
