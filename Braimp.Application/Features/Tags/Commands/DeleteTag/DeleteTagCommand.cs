using MediatR;

namespace Braimp.Application.Features.Tags.Commands.DeleteTag;
public class DeleteTagCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
