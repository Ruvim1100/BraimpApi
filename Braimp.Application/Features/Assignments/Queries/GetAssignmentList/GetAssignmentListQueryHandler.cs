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
        var assignments = dbContext.Assignments
            .Where(assignment => assignment.CourseId == request.CourseId);

        var result = await assignments.ProjectTo<AssignmentLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        return new AssignmentListResponse { Assignments = result };
    }
}
