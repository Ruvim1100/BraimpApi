using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Modules.Commands.UpdateModule;
public class UpdateModuleCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateModuleCommand, Unit>
{
    public async Task<Unit> Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
    {
        var module = await dbContext.Modules
            .FirstAsync(module => module.Id == request.Id, cancellationToken);

        module!.Title = request.Title ?? module.Title;
        module.Description = request.Description ?? module.Description;
        module.IsPublished = request.IsPublished ?? module.IsPublished;
        module.SortIndex = request.SortIndex ?? module.SortIndex;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
