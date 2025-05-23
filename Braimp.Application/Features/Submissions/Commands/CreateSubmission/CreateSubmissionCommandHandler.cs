using Braimp.Application.Abstraction;
using Braimp.Domain.Entities.Assignments;
using MediatR;

namespace Braimp.Application.Features.Submissions.Commands.CreateSubmission;
public class CreateSubmissionCommandHandler(IBraimpDbContext dbContext, IUnitOfWork unitOfWork) 
    : IRequestHandler<CreateSubmissionCommand, Guid>
{
    public async Task<Guid> Handle(CreateSubmissionCommand request, CancellationToken cancellationToken)
    {
        var submission = new Submission
        {
            Id = Guid.NewGuid(),
            StudentId = request.StudentId,
            Text = request.Text,
            CanEdit = false,
            AssignmentId = request.AssignmentId
        };

        dbContext.Submissions.Add(submission);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return submission.Id;
    }
}
