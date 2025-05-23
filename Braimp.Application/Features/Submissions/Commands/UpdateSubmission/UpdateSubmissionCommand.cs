using MediatR;

namespace Braimp.Application.Features.Submissions.Commands.UpdateSubmission;
public class UpdateSubmissionCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? Text { get; set; }
    public Guid AssignmentId { get; set; }
    public Guid CourseId { get; set; }
}
