using MediatR;

namespace Braimp.Application.Features.Assignments.Commands.DeleteAssignment;
public class DeleteAssignmentCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
}
