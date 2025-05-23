using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Assignments;
using MediatR;

namespace Braimp.Application.Features.Assignments.Commands.CreateAssignment;
public class CreateAssignmentCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateAssignmentCommand, Guid>
{
    public async Task<Guid> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = new Assignment
        { 
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Deadline = request.Deadline,
            CourseId = request.CourseId,

        };

        dbContext.Assignments.Add(assignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return assignment.Id;
    }
}