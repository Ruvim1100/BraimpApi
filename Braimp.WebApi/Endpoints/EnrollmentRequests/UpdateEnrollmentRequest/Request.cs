using Braimp.Domain.Entities.Courses.Enums;

namespace Braimp.WebApi.Endpoints.EnrollmentRequests.UpdateEnrollmentRequest;
public class Request
{
    public EnrollmentStatus Status { get; set; }
    public Guid UserId { get; set; }
}
