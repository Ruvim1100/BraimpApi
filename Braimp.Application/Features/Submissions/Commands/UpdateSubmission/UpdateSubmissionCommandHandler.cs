using Braimp.Application.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Braimp.Application.Features.Submissions.Commands.UpdateSubmission;
public class UpdateSubmissionCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) : IRequestHandler<UpdateSubmissionCommand, Guid>
{
    public async Task<Guid> Handle(UpdateSubmissionCommand request, CancellationToken cancellationToken)
    {
        var submission = await dbContext.Submissions
            .FirstAsync(submission => submission.Id == request.Id, cancellationToken);

        submission.Text = request.Text ?? submission.Text;
        dbContext.Submissions.Update(submission);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return submission.Id;
    }
}
