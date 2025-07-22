using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Tags;
using MediatR;

namespace Braimp.Application.Features.Tags.Commands.CreateTag;
public class CreateTagCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) : IRequestHandler<CreateTagCommand, Unit>
{
    public async Task<Unit> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new Tag
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        dbContext.Tags.Add(tag);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
