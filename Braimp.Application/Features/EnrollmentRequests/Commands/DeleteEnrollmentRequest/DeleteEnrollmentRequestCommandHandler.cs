using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.EnrollmentRequests.Commands.DeleteEnrollmentRequest;
public class DeleteEnrollmentRequestCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteEnrollmentRequestCommand, Unit>
{
    public async Task<Unit> Handle(DeleteEnrollmentRequestCommand request, CancellationToken cancellationToken)
    {
        var enrollmentRequest = await dbContext.EnrollmentRequests
            .FirstAsync(enrollmentRequest => enrollmentRequest.Id == request.Id, 
            cancellationToken);

        dbContext.EnrollmentRequests.Remove(enrollmentRequest);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
