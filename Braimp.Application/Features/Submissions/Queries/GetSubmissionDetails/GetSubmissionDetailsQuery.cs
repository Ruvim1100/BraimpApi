using MediatR;

namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionDetails;
public class GetSubmissionDetailsQuery : IRequest<SubmissionDetailsResponse>
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public Guid CourseId { get; set; }
}
