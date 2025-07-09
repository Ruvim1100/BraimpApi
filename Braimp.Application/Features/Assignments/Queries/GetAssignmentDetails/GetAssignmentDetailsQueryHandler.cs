using AutoMapper;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Assignments.Queries.GetAssignmentDetails;
public class GetAssignmentDetailsQueryHandler(IBraimpDbContext dbContext, IMapper mapper, IBlobStorageService blobStorageService) 
    : IRequestHandler<GetAssignmentDetailsQuery, AssignmentDetailsResponse>
{
    public async Task<AssignmentDetailsResponse> Handle(GetAssignmentDetailsQuery request, CancellationToken cancellationToken)
    {
        var assignment = await dbContext.Assignments
            .AsNoTracking()
            .Include(assignment => assignment.AssignmentFiles)
            .FirstAsync(assignment => assignment.Id == request.Id, cancellationToken);

        var result = mapper.Map<AssignmentDetailsResponse>(assignment);

        if (assignment.AssignmentFiles.Any())
        {
            var resourceIds = assignment.AssignmentFiles.Select(file => file.ResourceId);
            var resources = await dbContext.Resources
                .Where(resource => resourceIds.Contains(resource.Id))
                .ToListAsync(cancellationToken);

            var files = new List<AssignmentFileModel>();

            foreach (var assignmentFile in assignment.AssignmentFiles)
            {
                var resource = resources.FirstOrDefault(r => r.Id == assignmentFile.ResourceId);
                if (resource is null) continue;

                var (_, downloadToken) = blobStorageService.GetDownloadTokens(
                    containerName: "assignments",
                    blobName: resource.Url,
                    fileName: resource.Name,
                    expiry: TimeSpan.FromMinutes(5));

                files.Add(new AssignmentFileModel
                {
                    Id = assignmentFile.Id,
                    Name = resource.Name,
                    DownloadUrl = downloadToken
                });
            }

            result.Files = files;
        }

        return result;

    }
}
