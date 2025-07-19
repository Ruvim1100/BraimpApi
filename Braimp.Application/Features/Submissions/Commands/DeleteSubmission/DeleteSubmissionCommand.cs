using MediatR;

namespace Braimp.Application.Features.Submissions.Commands.DeleteSubmission;
public class DeleteSubmissionCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public Guid CourseId { get; set; }
}
