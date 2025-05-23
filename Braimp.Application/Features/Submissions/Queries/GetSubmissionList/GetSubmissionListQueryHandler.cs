using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionList;
public class GetSubmissionListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) 
    : IRequestHandler<GetSubmissionListQuery, SubmissionListResponse>
{
    public async Task<SubmissionListResponse> Handle(GetSubmissionListQuery request, CancellationToken cancellationToken)
    {
        var submissions = await dbContext.Submissions
            .Where(submission => submission.AssignmentId == request.AssignmentId)
            .ProjectTo<SubmissionLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        return new SubmissionListResponse { Submissions = submissions};
    }
}
