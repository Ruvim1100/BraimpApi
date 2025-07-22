using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Tags.Commands.DeleteTag;
public class DeleteTagCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) : IRequestHandler<DeleteTagCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await dbContext.Tags.FirstAsync(tag => tag.Id == request.Id, cancellationToken);

        dbContext.Tags.Remove(tag);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
