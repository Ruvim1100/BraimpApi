using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.EnrollmentRequests.Queries.GetEnrollmentRequestList;
public class GetEnrollmentRequestListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) 
    : IRequestHandler<GetEnrollmentRequestListQuery, EnrollmentRequestListResponse>
{
    public async Task<EnrollmentRequestListResponse> Handle(GetEnrollmentRequestListQuery request, CancellationToken cancellationToken)
    {
        var enrollments = await dbContext.EnrollmentRequests
            .Where(enrollment => enrollment.CourseId == request.CourseId)
            .Include(enrollment => enrollment.User)
            .ProjectTo<EnrollmentRequestLookupModel>(mapper.ConfigurationProvider)
            .OrderBy(enrollment => enrollment.CreatedAt)
            .ToListAsync(cancellationToken);

        return new EnrollmentRequestListResponse
        {
            Enrollments = enrollments
        };
    }
}
