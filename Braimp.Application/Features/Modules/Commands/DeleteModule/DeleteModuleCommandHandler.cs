using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Commands.DeleteModule;
public class DeleteModuleCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteModuleCommand, Unit>
{
    public async Task<Unit> Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
    {
        var module = await dbContext.Modules
            .FirstAsync(module => module.Id == request.Id, cancellationToken);

        var courseId = module.CourseId;
        var deletedSortIndex = module.SortIndex;

        dbContext.Modules.Remove(module);

        var modulesToUpdate = await dbContext.Modules
            .Where(module => module.CourseId == courseId && module.SortIndex > deletedSortIndex)
            .ToListAsync(cancellationToken);

        foreach (var moduleToUpdate in modulesToUpdate)
        {
            moduleToUpdate.SortIndex -= 1;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
