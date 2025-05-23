using MediatR;

namespace Braimp.Application.Features.Submissions.Commands.CreateSubmission;
public class CreateSubmissionCommand : IRequest<Guid>
{
    public Guid StudentId { get; set; }
    public string? Text { get; set; }
    public Guid AssignmentId { get; set; }
    public Guid CourseId { get; set; }
}
