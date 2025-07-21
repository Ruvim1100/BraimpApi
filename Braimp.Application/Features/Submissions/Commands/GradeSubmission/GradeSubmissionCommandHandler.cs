using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Submissions.Commands.GradeSubmission;
public class GradeSubmissionCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork, ICurrentUserService currentUserService) 
    : IRequestHandler<GradeSubmissionCommand, Unit>
{
    public async Task<Unit> Handle(GradeSubmissionCommand request, CancellationToken cancellationToken)
    {
        var submission = await dbContext.Submissions
            .FirstAsync(submission => submission.Id == request.Id, cancellationToken);

        submission.ReviewedAt = DateTimeOffset.UtcNow;
        submission.ReviewerId = currentUserService.UserId;

        submission.Grade = request.Grade ?? submission.Grade;
        submission.ReviewComment = request.ReviewComment ?? submission.ReviewComment;

        dbContext.Submissions.Update(submission);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
