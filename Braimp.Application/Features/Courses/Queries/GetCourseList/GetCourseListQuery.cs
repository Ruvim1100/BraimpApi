using Braimp.Application.Pagination;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
public class GetCourseListQuery : PaginationRequest<CourseListResponse.Item>
{
    public string? SearchTerm { get; set; }
    public Guid? Category { get; set; }
    public string? Status { get; set; } = CourseStatus.Approved.ToString();
}
