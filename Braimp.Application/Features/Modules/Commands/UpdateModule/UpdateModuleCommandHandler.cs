using Braimp.Application.Abstraction;
using Braimp.Application.Common.Exceptions;
using Braimp.Domain.Entities.Courses.Enums;
using Braimp.Domain.Entities.LearningContent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Commands.UpdateModule;
public class UpdateModuleCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, 
    ICurrentUserService currentUser, ICourseAuthorizationService courseAuthorizationService) 
    : IRequestHandler<UpdateModuleCommand, Unit>
{
    public async Task<Unit> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
    {
        var module = await dbContext.Modules
            .FirstOrDefaultAsync(module => module.Id == request.Id, cancellationToken);

        if (module is null)
            throw new NotFoundException(nameof(Module), request.Id);

        await courseAuthorizationService.EnsureUserHasRole(module.CourseId, currentUser.UserId, 
            CourseRole.Owner,CourseRole.Assistant);

        module.Title = request.Title ?? module.Title;
        module.Description = request.Description ?? module.Description;
        module.IsVisibleToStudent = request.IsVisibleToStudent ?? module.IsVisibleToStudent;
        module.SortIndex = request.SortIndex ?? module.SortIndex;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
