namespace Braimp.Application.Features.SubmissionFiles.Queries.GetSubmissionFileList;
public class SubmissionFileLookupModel
{
    public Guid SubmissionFileId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DownloadUrl { get; set; } = string.Empty;
}
