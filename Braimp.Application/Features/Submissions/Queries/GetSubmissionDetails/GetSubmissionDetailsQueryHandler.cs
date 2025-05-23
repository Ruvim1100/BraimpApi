using AutoMapper;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionDetails;
public class GetSubmissionDetailsQueryHandler(IBraimpDbContext dbContext, IMapper mapper) 
    : IRequestHandler<GetSubmissionDetailsQuery, SubmissionDetailsResponse>
{
    public async Task<SubmissionDetailsResponse> Handle(GetSubmissionDetailsQuery request, CancellationToken cancellationToken)
    {
        var submission = await dbContext.Submissions
            .FirstAsync(submission => submission.Id == request.Id, cancellationToken);

        return mapper.Map<SubmissionDetailsResponse>(submission);
    }
}
