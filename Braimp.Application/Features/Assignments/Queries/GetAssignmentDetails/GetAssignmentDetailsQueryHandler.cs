using AutoMapper;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Assignments.Queries.GetAssignmentDetails;
public class GetAssignmentDetailsQueryHandler(IBraimpDbContext dbContext, IMapper mapper) 
    : IRequestHandler<GetAssignmentDetailsQuery, AssignmentDetailsResponse>
{
    public async Task<AssignmentDetailsResponse> Handle(GetAssignmentDetailsQuery request, CancellationToken cancellationToken)
    {
        var assignment = await dbContext.Assignments
            .FirstAsync(assignment => assignment.Id == request.Id);
        return mapper.Map<AssignmentDetailsResponse>(assignment);
    }
}
