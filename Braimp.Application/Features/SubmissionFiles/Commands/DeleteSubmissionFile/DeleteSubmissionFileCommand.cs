using MediatR;

namespace Braimp.Application.Features.SubmissionFiles.Commands.DeleteSubmissionFile;
public class DeleteSubmissionFileCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid SubmissionId { get; set; }
}
