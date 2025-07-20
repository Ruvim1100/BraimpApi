using MediatR;

namespace Braimp.Application.Features.Courses.Commands.DeleteParticipant;
public class DeleteParticipantCommand : IRequest<Unit>
{
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }
}
