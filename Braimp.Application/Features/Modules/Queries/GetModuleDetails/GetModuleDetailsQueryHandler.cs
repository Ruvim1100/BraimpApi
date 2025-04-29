using Braimp.Application.Abstraction;
using Braimp.Application.Common.Exceptions;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Braimp.Domain.Entities.LearningContent;

namespace Braimp.Application.Features.Modules.Queries.GetModuleDetails;
public class GetModuleDetailsQueryHandler(IBraimpDbContext dbContext, IMapper mapper, 
    ICurrentUserService currentUser, ICourseAuthorizationService courseAuthorizationService) 
    : IRequestHandler<GetModuleDetailsQuery, ModuleDetailsResponse>
{
    public async Task<ModuleDetailsResponse> Handle(GetModuleDetailsQuery request, CancellationToken cancellationToken)
    {
        var module = await dbContext.Modules
            .FirstOrDefaultAsync(module => module.Id == request.Id);

        if (module is null)
            throw new NotFoundException(nameof(Module), request.Id);

        await courseAuthorizationService.EnsureUserHasRole(module.CourseId, currentUser.UserId, 
            CourseRole.Owner, CourseRole.Assistant);

        return mapper.Map<ModuleDetailsResponse>(module);
    }
}
