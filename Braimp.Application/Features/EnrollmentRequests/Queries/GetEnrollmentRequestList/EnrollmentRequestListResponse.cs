namespace Braimp.Application.Features.EnrollmentRequests.Queries.GetEnrollmentRequestList;
public class EnrollmentRequestListResponse
{
    public List<EnrollmentRequestLookupModel> Enrollments { get; set; } = new List<EnrollmentRequestLookupModel>();
}
