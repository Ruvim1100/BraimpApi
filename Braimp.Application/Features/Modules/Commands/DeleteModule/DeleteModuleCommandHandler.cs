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

        dbContext.Modules.Remove(module);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
