using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Courses.Commands.DeleteParticipant;
public class DeleteParticipantCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteParticipantCommand, Unit>
{
    public async Task<Unit> Handle(DeleteParticipantCommand request, CancellationToken cancellationToken)
    {
        var participant = await dbContext.CourseParticipants
            .FirstAsync(participant => participant.UserId == request.UserId &&
            participant.CourseId == request.CourseId, 
            cancellationToken);

        dbContext.CourseParticipants.Remove(participant);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
