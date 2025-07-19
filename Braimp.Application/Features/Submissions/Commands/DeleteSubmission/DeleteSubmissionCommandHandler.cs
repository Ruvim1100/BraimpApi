using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Submissions.Commands.DeleteSubmission;
public class DeleteSubmissionCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeleteSubmissionCommand, Unit>
{
    public async Task<Unit> Handle(DeleteSubmissionCommand request, CancellationToken cancellationToken)
    {
        var submission = await dbContext.Submissions
            .FirstAsync(submission => submission.Id == request.Id, cancellationToken);

        dbContext.Submissions.Remove(submission);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
