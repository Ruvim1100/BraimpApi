using Braimp.Application.Abstraction;
using Braimp.Application.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.AssignmentFiles.Queries.GetAssignmentFileList;
public class GetAssignmentFileListQueryHandler(IBraimpDbContext dbContext, IBlobStorageService blobStorageService) 
    : IRequestHandler<GetAssignmentFileListQuery, AssignmentFileListResponse>
{
    public async Task<AssignmentFileListResponse> Handle(GetAssignmentFileListQuery request, CancellationToken cancellationToken)
    {
        var files = await dbContext.AssignmentFiles
            .Where(assignmentFile => assignmentFile.AssignmentId == request.AssignmentId)
            .Join(dbContext.Resources, 
            assignmentFile => assignmentFile.ResourceId,
            resource => resource.Id,
            (assignmentFile, resource) => new {AssignmentFile = assignmentFile, Resource = resource})
            .ToListAsync(cancellationToken);

        var assignmentFiles = new List<AssignmentFileLookupModel>();

        foreach (var file in files)
        {
            var sasUrl = blobStorageService.GetDownloadTokens(
                containerName: BlobContainers.Assignments,
                blobName: file.Resource.Url,
                fileName: file.Resource.Name + Path.GetExtension(file.Resource.Url),
                expiry: TimeSpan.FromMinutes(10));

            assignmentFiles.Add(
                new AssignmentFileLookupModel
                {
                    AssignmentFileId = file.AssignmentFile.Id,
                    Name = file.Resource.Name,
                    DownloadUrl = sasUrl.DownloadToken,
                    PreviewUrl = sasUrl.PreviewToken
                }
            );
        }

        return new AssignmentFileListResponse { AssignmentFiles = assignmentFiles};
    }
}
