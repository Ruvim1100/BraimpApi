using MediatR;

namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionList;
public class GetSubmissionListQuery : IRequest<SubmissionListResponse>
{
    public Guid AssignmentId { get; set; }
    public Guid CourseId { get; set; }
}
