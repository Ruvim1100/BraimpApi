namespace Braimp.Application.Features.SubmissionFiles.Queries.GetSubmissionFileList;
public class SubmissionFileListResponse
{
    public List<SubmissionFileLookupModel> SubmissionFiles { get; set; } = new();
}
