using AutoMapper;
using AutoMapper.QueryableExtensions;
using Braimp.Application.Abstraction;
using Braimp.Application.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Queries.GetModuleList;
public class GetModuleListQueryHandler(IBraimpDbContext dbContext, IMapper mapper,
    ICurrentUserService currentUser, ICourseAuthorizationService courseAuthorizationService) 
    : IRequestHandler<GetModuleListQuery, ModuleListResponse>
{
    public async Task<ModuleListResponse> Handle(GetModuleListQuery request, CancellationToken cancellationToken)
    {
        await courseAuthorizationService.EnsureUserIsCourseParticipant(request.CourseId, currentUser.UserId);

        var query = dbContext.Modules
            .Where(module => module.CourseId == request.CourseId);

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            var pattern = $"%{request.SearchTerm}%";
            query = query.Where(module => EF.Functions.Like(module.Title, pattern) ||
                (module.Description != null && EF.Functions.Like(module.Description, pattern))
            );
        }

        if (request.IsPublished.HasValue)
            query = query.Where(m => m.IsPublished == request.IsPublished);

        var modules = await query.OrderBy(module => module.SortIndex)
            .ProjectTo<ModuleLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ModuleListResponse { Modules = modules};
    }
}
