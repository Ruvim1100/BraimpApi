using MediatR;

namespace Braimp.Application.Features.SubmissionFiles.Commands.UpdateSubmissionFile;
public class UpdateSubmissionFileCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid SubmissionId { get; set; }
    public string Name { get; set; } = string.Empty;
}
