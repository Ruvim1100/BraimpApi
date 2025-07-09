using MediatR;

namespace Braimp.Application.Features.Courses.Queries.GetStudentList;
public class GetStudentListQuery : IRequest<StudentListResponse>
{
    public Guid CourseId { get; set; }
}
