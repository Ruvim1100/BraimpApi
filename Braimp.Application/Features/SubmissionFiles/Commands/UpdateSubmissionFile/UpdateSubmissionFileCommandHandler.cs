using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.SubmissionFiles.Commands.UpdateSubmissionFile;
public class UpdateSubmissionFileCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateSubmissionFileCommand, Guid>
{
    public async Task<Guid> Handle(UpdateSubmissionFileCommand request, CancellationToken cancellationToken)
    {
        var submissionFile = await dbContext.SubmissionFiles
            .FirstAsync(submissionFile => submissionFile.Id == request.Id, cancellationToken);

        var resource = await dbContext.Resources
            .FirstAsync(resource => resource.Id == submissionFile.ResourceId, cancellationToken);

        resource.Name = request.Name;

        dbContext.Resources.Update(resource);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return resource.Id;
    }
}
