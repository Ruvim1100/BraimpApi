using Braimp.Application.Pagination;

namespace Braimp.Application.Features.Courses.Queries.GetEnrolledCourseList;
public class EnrolledCourseListResponse : PaginationResult<EnrollmentRequestLookupModel>
{
    public EnrolledCourseListResponse(List<EnrollmentRequestLookupModel> items, int page, int pageSize, int totalCount) : base(items, page, pageSize, totalCount)
    {
    }

}
