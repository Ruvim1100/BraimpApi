using Braimp.Application.Pagination;

namespace Braimp.Application.Features.Courses.Queries.GetEnrolledCourseList;
public class EnrolledCourseListResponse : PaginationResult<EnrolledCourseLookupModel>
{
    public EnrolledCourseListResponse(List<EnrolledCourseLookupModel> items, int page, int pageSize, int totalCount) : base(items, page, pageSize, totalCount)
    {
    }

}
