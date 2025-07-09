using AutoMapper;
using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Submissions.Queries.GetSubmissionDetails;
public class GetSubmissionDetailsQueryHandler(IBraimpDbContext dbContext, IMapper mapper, IBlobStorageService blobStorageService) 
    : IRequestHandler<GetSubmissionDetailsQuery, SubmissionDetailsResponse>
{
    public async Task<SubmissionDetailsResponse> Handle(GetSubmissionDetailsQuery request, CancellationToken cancellationToken)
    {
        var submission = await dbContext.Submissions
            .FirstAsync(submission => submission.Id == request.Id, cancellationToken);

        var submissionDetailsRespone = mapper.Map<SubmissionDetailsResponse>(submission);

        var student = await dbContext.Users
            .FirstOrDefaultAsync(user => user.Id == submission.StudentId,
            cancellationToken);

        if (student != null)
        {
            submissionDetailsRespone.Student = new StudentModel
            {
                Id = student.Id,
                Name = student.Name,
                Surname = student.Surname
            };
        }

        var resource = await dbContext.Resources
            .FirstOrDefaultAsync(resource => resource.Id == submission.FileResourceId,
             cancellationToken);

        if (resource != null)
        {
            var fileUrl = blobStorageService
            .GetDownloadTokens(BlobContainers.Submissions,
            resource.Url, resource.Name, TimeSpan.FromMinutes(10))
            .DownloadToken;

            submissionDetailsRespone.DownloadFileUrl = fileUrl;
        }

        return submissionDetailsRespone;
    }
}
