using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.SubmissionFiles.Queries.GetSubmissionFileList;
public class GetSubmissionFileListQueryHandler(IBraimpDbContext dbContext, IBlobStorageService blobStorageService) 
    : IRequestHandler<GetSubmissionFileListQuery, SubmissionFileListResponse>
{
    public async Task<SubmissionFileListResponse> Handle(GetSubmissionFileListQuery request, CancellationToken cancellationToken)
    {
        var files = await dbContext.SubmissionFiles
            .Where(submissionFile => submissionFile.SubmissionId == request.SubmissionId)
            .Join(dbContext.Resources,
            submissionFile => submissionFile.ResourceId,
            resource => resource.Id,
            (submissionFile, resource) => new { SubmissionFile = submissionFile, Resource = resource})
            .ToListAsync(cancellationToken);

        var submissionFiles = new List<SubmissionFileLookupModel>();

        foreach (var file in files)
        {
            var sasUrl = await blobStorageService.GenerateSasUriAsync(
                containerName: BlobContainers.Submissions,
                blobName: file.Resource.Url,
                expiry: TimeSpan.FromMinutes(10));

            submissionFiles.Add(
                new SubmissionFileLookupModel
                {
                    SubmissionFileId = file.SubmissionFile.Id,
                    Name = file.Resource.Name,
                    DownloadUrl = sasUrl.ToString()
                });
        }

        return new SubmissionFileListResponse { SubmissionFiles = submissionFiles };
    }
}
