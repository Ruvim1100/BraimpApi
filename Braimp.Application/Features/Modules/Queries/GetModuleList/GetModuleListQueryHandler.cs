using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Queries.GetModuleList;
public class GetModuleListQueryHandler(IBraimpDbContext dbContext, IMapper mapper) 
    : IRequestHandler<GetModuleListQuery, ModuleListResponse>
{
    public async Task<ModuleListResponse> Handle(GetModuleListQuery request, CancellationToken cancellationToken)
    {
        var modules = await dbContext.Modules
            .Include(m => m.Lessons)
            .Where(module => module.CourseId == request.CourseId)
            .OrderBy(module => module.SortIndex)
            .ProjectTo<ModuleLookupModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken); ;
        return new ModuleListResponse { Modules = modules};
    }
}
