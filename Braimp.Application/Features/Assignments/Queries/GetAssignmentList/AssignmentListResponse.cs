namespace Braimp.Application.Features.Assignments.Queries.GetAssignmentList;
public class AssignmentListResponse
{
    public List<AssignmentLookupModel> Assignments { get; set; } = new();
}
