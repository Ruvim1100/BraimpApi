using Braimp.Domain.Entities.Courses.Enums;
using MediatR;

namespace Braimp.Application.Features.Courses.Queries.GetCourseList;
public class GetCourseListQuery : IRequest<CourseListResponse> 
{
    public string? SearchTerm { get; set; }
    public Guid? Category { get; set; }
    public string? Status { get; set; } = CourseStatus.Approved.ToString();
    public string? SortBy { get; set; } = "CreatedAt";
    public bool? Descending { get; set; } = false;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
