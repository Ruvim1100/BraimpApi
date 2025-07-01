using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Queries.GetPublishedModuleList;
public class GetPublishedModuleListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) : IRequestHandler<GetPublishedModuleListQuery, PublishedModuleListResponse>
{
    public async Task<PublishedModuleListResponse> Handle(GetPublishedModuleListQuery request, CancellationToken cancellationToken)
    {
        var modules = await dbContext.Modules
            .Where(module => module.CourseId == request.CourseId && module.IsPublished)
            .OrderBy(module => module.SortIndex)
            .ProjectTo<PublishedModuleLookupModule>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PublishedModuleListResponse { Modules = modules };
    }
}
