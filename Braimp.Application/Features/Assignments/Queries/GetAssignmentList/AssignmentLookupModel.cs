namespace Braimp.Application.Features.Assignments.Queries.GetAssignmentList;
public class AssignmentLookupModel
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset Deadline { get; set; }
    public Guid CourseId { get; set; }
}
