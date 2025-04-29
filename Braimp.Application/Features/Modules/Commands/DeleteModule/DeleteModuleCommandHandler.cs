using Braimp.Application.Abstraction;
using Braimp.Application.Common.Exceptions;
using Braimp.Domain.Entities.Courses.Enums;
using Braimp.Domain.Entities.LearningContent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Commands.DeleteModule;
public class DeleteModuleCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork,
    ICurrentUserService currentUser, ICourseAuthorizationService courseAuthorizationService) 
    : IRequestHandler<DeleteModuleCommand, Unit>
{
    public async Task<Unit> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
    {
        var module = await dbContext.Modules
            .FirstOrDefaultAsync(module => module.Id == request.Id, cancellationToken);

        if (module is null)
            throw new NotFoundException(nameof(Module), request.Id);

        await courseAuthorizationService.EnsureUserHasRole(module.CourseId, currentUser.UserId,
            CourseRole.Owner, CourseRole.Assistant);

        dbContext.Modules.Remove(module);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
