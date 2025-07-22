using MediatR;

namespace Braimp.Application.Features.Tags.Commands.CreateTag;
public class CreateTagCommand : IRequest<Unit>
{
    public string Name { get; set; } = string.Empty;
}
