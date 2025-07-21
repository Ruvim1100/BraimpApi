using MediatR;

namespace Braimp.Application.Features.Submissions.Commands.GradeSubmission;
public class GradeSubmissionCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public decimal? Grade { get; set; }
    public string? ReviewComment { get; set; }
    public Guid AssignmentId { get; set; }
    public Guid CourseId { get; set; }
}
