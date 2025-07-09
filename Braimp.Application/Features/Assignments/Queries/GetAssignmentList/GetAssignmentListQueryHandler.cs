using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Assignments.Queries.GetAssignmentList;
public class GetAssignmentListQueryHandler(IBraimpDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAssignmentListQuery, AssignmentListResponse>
{
    public async Task<AssignmentListResponse> Handle(GetAssignmentListQuery request, CancellationToken cancellationToken)
    {
        var assignments = await dbContext.Assignments
            .Where(assignment => assignment.CourseId == request.CourseId)
            .OrderBy(assignment => assignment.Deadline)
            .ProjectTo<AssignmentLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new AssignmentListResponse { Assignments = assignments };
    }
}
