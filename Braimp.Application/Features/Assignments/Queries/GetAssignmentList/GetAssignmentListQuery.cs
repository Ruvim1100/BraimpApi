using MediatR;

namespace Braimp.Application.Features.Assignments.Queries.GetAssignmentList;
public class GetAssignmentListQuery : IRequest<AssignmentListResponse>
{
    public Guid CourseId { get; set; }
}
