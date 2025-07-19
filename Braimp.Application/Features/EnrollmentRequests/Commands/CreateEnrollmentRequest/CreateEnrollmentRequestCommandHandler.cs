using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Courses;
using Braimp.Domain.Entities.Courses.Enums;
using MediatR;

namespace Braimp.Application.Features.EnrollmentRequests.Commands.CreateEnrollmentRequest;
public class CreateEnrollmentRequestCommandHandler(IBraimpDbContext dbContext, 
    IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<CreateEnrollmentRequestCommand, Unit>
{
    public async Task<Unit> Handle(CreateEnrollmentRequestCommand request, CancellationToken cancellationToken)
    {
        var enrollment = new EnrollmentRequest
        {
            Id = Guid.NewGuid(),
            UserId = currentUser.UserId,
            CourseId = request.CourseId,
            Status = EnrollmentStatus.Pending
        };

        dbContext.EnrollmentRequests.Add(enrollment);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
