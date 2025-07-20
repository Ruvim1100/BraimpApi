using Braimp.Domain.Entities.Courses.Enums;
using MediatR;

namespace Braimp.Application.Features.EnrollmentRequests.Commands.UpdateEnrollmentRequest;
public class UpdateEnrollmentRequestCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public Guid CourseId {  get; set; }
    public Guid UserId { get; set; }
    public EnrollmentStatus Status { get; set; }
}
