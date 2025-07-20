using MediatR;

namespace Braimp.Application.Features.EnrollmentRequests.Queries.GetEnrollmentRequestList;
public class GetEnrollmentRequestListQuery : IRequest<EnrollmentRequestListResponse>
{
    public Guid CourseId { get; set; }
}
