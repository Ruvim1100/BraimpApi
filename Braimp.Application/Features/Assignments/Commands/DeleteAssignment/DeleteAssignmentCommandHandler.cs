using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Assignments.Commands.DeleteAssignment;
public class DeleteAssignmentCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteAssignmentCommand, Unit>>
{
    public async Task<Unit> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await dbContext.Assignments
            .FirstAsync(assignment => assignment.Id == request.Id, cancellationToken);

        dbContext.Assignments.Remove(assignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
