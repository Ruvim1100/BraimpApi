using MediatR;

namespace Braimp.Application.Features.Assignments.Queries.GetAssignmentDetails;
public class GetAssignmentDetailsQuery : IRequest<AssignmentDetailsResponse>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
}
