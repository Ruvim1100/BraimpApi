using AutoMapper;
using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Queries.GetModuleDetails;
public class GetModuleDetailsQueryHandler(IBraimpDbContext dbContext, IMapper mapper) 
    : IRequestHandler<GetModuleDetailsQuery, ModuleDetailsResponse>
{
    public async Task<ModuleDetailsResponse> Handle(GetModuleDetailsQuery request, CancellationToken cancellationToken)
    {
        var module = await dbContext.Modules
            .FirstAsync(module => module.Id == request.Id);

        return mapper.Map<ModuleDetailsResponse>(module);
    }
}
