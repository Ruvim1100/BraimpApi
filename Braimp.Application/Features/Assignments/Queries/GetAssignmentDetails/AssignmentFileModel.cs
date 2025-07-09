namespace Braimp.Application.Features.Assignments.Queries.GetAssignmentDetails;
public class AssignmentFileModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DownloadUrl { get; set; } = string.Empty;
}
