namespace Braimp.Application.Features.AssignmentFiles.Queries.GetAssignmentFileList;
public class AssignmentFileLookupModel
{
    public Guid AssignmentFileId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DownloadUrl { get; set; } = string.Empty;
}
