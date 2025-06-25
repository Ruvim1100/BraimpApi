using Braimp.Application.Pagination;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
public class GetCourseListQuery : PaginationRequest<CourseLookupModel>
{
    public string? SearchTerm { get; set; }
    public Guid? Category { get; set; }
}
