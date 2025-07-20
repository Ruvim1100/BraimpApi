using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.EnrollmentRequests.Commands.UpdateEnrollmentRequest;
public class UpdateEnrollmentRequestCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<UpdateEnrollmentRequestCommand, Unit>
{
    public async Task<Unit> Handle(UpdateEnrollmentRequestCommand request, CancellationToken cancellationToken)
    {
        var enrollmentRequest = await dbContext.EnrollmentRequests
            .FirstAsync(enrollmentRequest => enrollmentRequest.Id == request.Id, 
            cancellationToken);

        enrollmentRequest.Status = request.Status;

        if (request.Status == EnrollmentStatus.Approved)
        {
            var participant = new CourseParticipant
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                CourseId = request.CourseId,
                Role = CourseRole.Student
            };

            dbContext.CourseParticipants.Add(participant);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
