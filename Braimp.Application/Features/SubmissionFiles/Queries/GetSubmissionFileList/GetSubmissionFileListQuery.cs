using MediatR;

namespace Braimp.Application.Features.SubmissionFiles.Queries.GetSubmissionFileList;
public class GetSubmissionFileListQuery : IRequest<SubmissionFileListResponse>
{
    public Guid SubmissionId { get; set; }
    public Guid AssignmentId { get; set; }
}
