using Braimp.Application.Pagination;

namespace Braimp.Application.Features.Courses.Queries.GetOwnedCourseList;
public class OwnedCourseListResponse : PaginationResult<OwnedCourseLookupModel>
{
    public OwnedCourseListResponse(List<OwnedCourseLookupModel> items, int page, int pageSize, int totalCount) 
        : base (items, page, pageSize, totalCount ) { }
}
