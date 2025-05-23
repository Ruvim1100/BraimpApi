using Braimp.Application.Pagination;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
public class CourseListResponse : PaginationResult<CourseLookupModel>
{
    public CourseListResponse(List<CourseLookupModel> items, int page, int pageSize, int totalCount) : base(items, page, pageSize, totalCount)
    {
    }
}
