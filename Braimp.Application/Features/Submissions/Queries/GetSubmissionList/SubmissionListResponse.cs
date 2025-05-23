namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionList;
public class SubmissionListResponse
{
    public List<SubmissionLookupModel> Submissions { get; set; } = new();
}
