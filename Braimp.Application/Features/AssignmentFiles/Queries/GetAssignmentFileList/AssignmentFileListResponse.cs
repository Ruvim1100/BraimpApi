namespace Braimp.Application.Features.AssignmentFiles.Queries.GetAssignmentFileList;
public class AssignmentFileListResponse
{
    public List<AssignmentFileLookupModel> AssignmentFiles { get; set; } = new();
}
