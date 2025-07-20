using MediatR;

namespace Braimp.Application.Features.EnrollmentRequests.Commands.DeleteEnrollmentRequest;

public class DeleteEnrollmentRequestCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
}
