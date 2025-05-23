using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Assignments.Commands.UpdateAssignment;
public class UpdateAssignmentCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateAssignmentCommand, Guid>
{
    public async Task<Guid> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await dbContext.Assignments
            .FirstAsync(assignment => assignment.Id == request.Id, cancellationToken);

        assignment.Title = request.Title ?? assignment.Title;
        assignment.Description = request.Description ?? assignment.Description;
        assignment.Deadline = request.Deadline ?? assignment.Deadline;

        dbContext.Assignments.Update(assignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return assignment.Id;
    }
}
