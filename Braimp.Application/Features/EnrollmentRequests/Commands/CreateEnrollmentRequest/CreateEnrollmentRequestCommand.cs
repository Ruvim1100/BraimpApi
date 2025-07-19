using MediatR;

namespace Braimp.Application.Features.EnrollmentRequests.Commands.CreateEnrollmentRequest;
public class CreateEnrollmentRequestCommand : IRequest<Unit>
{
    public Guid CourseId { get; set; }
}
