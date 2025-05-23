using MediatR;

namespace Braimp.Application.Features.Assignments.Commands.CreateAssignment;
public class CreateAssignmentCommand : IRequest<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTimeOffset Deadline { get; set; }
    public Guid CourseId { get; set; }
}