using MediatR;

namespace Braimp.Application.Features.Assignments.Commands.UpdateAssignment;
public class UpdateAssignmentCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset? Deadline { get; set; }
    public Guid CourseId { get; set; }
}